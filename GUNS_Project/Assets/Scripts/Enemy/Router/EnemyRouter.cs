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
        foreach (var enemy in _enemies)
        {
            EnemyView view = enemy.Key;
            EnemyModel model = enemy.Value;

            var allies = EntityController.Instance.AllyEntities;
            
            if (allies.Count == 0) 
            {
                model.Movement = new ToPointMovement(model.StartPoint);
                view.MoveTo(model.Movement.GetPosition());
                continue;
            }

            AbstractEntity nearestAlly = null;
            float minDistance = 5;

            foreach (var ally in allies)
            {
                if (ally == null) continue;
                
                float distance = Vector3.Distance(view.transform.position, ally.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestAlly = ally;
                }
            }

            if (nearestAlly != null)
            {
                model.Movement = new ToPointMovement(nearestAlly.transform);
                view.MoveTo(model.Movement.GetPosition());
            }
            else
            {
                model.Movement = new ToPointMovement(model.StartPoint);
                view.MoveTo(model.Movement.GetPosition());
            }
        }
    }

    public void Exit()
    {
        
    }
}