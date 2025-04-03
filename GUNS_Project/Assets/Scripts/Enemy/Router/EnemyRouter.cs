using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyRouter : IRouter
{
    private Dictionary<EnemyView, EnemyModel> _enemies = new();
    
    private EnemyView _prefab;

    private EnemyWindow Window => UiController.Instance.GetWindow<EnemyWindow>();

    public void Init()
    {
        _prefab = Resources.Load<EnemyView>("Prefabs/Enemy");

        foreach (var point in Window.Points)
        {
           EnemyView enemy = Window.CreateEnemy(_prefab, point);
           
           EnemyModel model = new EnemyModel();
           model.StartPoint = point;
           
           _enemies.Add(enemy, model);
        }
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
            if (_enemies.Count == 0) return;
        
            var allies = EntityController.Instance.AllyEntities;
            bool hasAllies = allies.Count > 0;
        
            foreach (var enemyPair in EntityController.Instance.Enemies)
            {
                EnemyView view = enemyPair;
                EnemyModel model = _enemies[enemyPair];
                Vector3 currentPosition = view.transform.position;
        
                if (!hasAllies)
                {
                    model.Movement = new ToPointMovement(model.StartPoint);
                    view.MoveTo(model.Movement.GetPosition());
                    continue;
                }
        
                AbstractEntity nearestAlly = null;
                float minDistanceSqr = 25f;
        
                foreach (var ally in allies)
                {
                    if (ally == null) continue;
                    
                    float distanceSqr = (currentPosition - ally.transform.position).sqrMagnitude;
                    if (distanceSqr < minDistanceSqr)
                    {
                        minDistanceSqr = distanceSqr;
                        nearestAlly = ally;
                    }
                }
        
                if (nearestAlly != null)
                {
                    model.Movement = new ToPointMovement(nearestAlly.transform);

                    if (minDistanceSqr < 4 && (model.Attack == null || model.Attack.IsCooldown))
                    {
                        model.Attack = new MiddleAttack(5, nearestAlly);
                        model.Attack.Attack();
                    }
                    
                    view.MoveTo(model.Movement.GetPosition());
                }
                else
                {
                    float distanceSqr = (currentPosition - model.StartPoint.position).sqrMagnitude;

                    if (distanceSqr > 3)
                    {
                        model.Movement = new ToPointMovement(model.StartPoint);
                        view.MoveTo(model.Movement.GetPosition());
                    }
                }
                
            }
    }

    public void Exit()
    {
        
    }
}