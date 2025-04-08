using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyRouter : IRouter
{
    private EnemyView _prefab;

    private EnemyWindow Window => UiController.Instance.GetWindow<EnemyWindow>();

    public void Init()
    {
        _prefab = Resources.Load<EnemyView>("Prefabs/Enemy");

        foreach (var point in Window.Points)
        {
            EnemyModel model = new EnemyModel();
            model.StartPoint = point;
            
            EnemyView enemy = Window.CreateEnemy(_prefab, model, point);
        }
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
            // if (EntityController.Instance.Enemies.Count == 0) return;
        
            var allies = EntityController.Instance.AllyEntities;
            bool hasAllies = allies.Count > 0;
            
            foreach (var view in EntityController.Instance.Enemies)
            {
                EntityModel model = EntityController.Instance.FullEntities[view];
                Vector3 currentPosition = view.transform.position;
        
                if (hasAllies == false)
                {
                    IMovement movement = new ToPointMovement(model.StartPoint);
                    EntityController.Instance.FullEntities[view].Movement = movement;
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
                    if (minDistanceSqr < 20)
                    {
                        AttackController.Instance.UpdateAttack(view, new ShootAttack(5, view, nearestAlly));
                    }
                    
                    UpdateMovement(view, new ToPointMovement(nearestAlly.transform));
                }
                else
                {
                    float distanceSqr = (currentPosition - model.StartPoint.position).sqrMagnitude;

                    if (distanceSqr > 3)
                    {
                        UpdateMovement(view, new ToPointMovement(model.StartPoint));
                    }
                }
            }
    }

    private void UpdateMovement(AbstractEntity entity, IMovement movement) => 
        MovementController.Instance.UpdateMovement(entity, movement);

    public void Exit()
    {
        
    }
}