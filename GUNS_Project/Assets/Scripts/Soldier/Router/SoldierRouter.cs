using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRouter : IRouter
{
    private readonly Dictionary<SoldierView, IMovement> _soldiers = new();
    private int _freePointIndex;
    private SoldierView _prefab;

    private SoldierWindow Window => UiController.Instance.GetWindow<SoldierWindow>();

    private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;
    
    public void Init()
    {
        _prefab = Resources.Load<SoldierView>("Prefabs/Soldier");
        
        BarrackController.Instance.CreatedSoldier += OnCreated;
        BattleController.Instance.Restarted += OnRestarted;
        
        UpdateController.Instance.Add(OnUpdate);
        
    }
    
    private void OnRestarted()
    {
        _freePointIndex = 0;
    }

    private void OnCreated(Transform point)
    {
        List<Transform> points = AllyPoint.MoveToPoints;
        
        if (_freePointIndex >= points.Count || BattleController.Instance.IsMoving)
        {
            return;
        }
        
        SoldierModel model = new SoldierModel();

        SoldierView soldier = Window.CreateSolider(_prefab, point, model);

        IMovement movement = new ToPointMovement(points[_freePointIndex]);

        _soldiers.Add(soldier, movement);

        model.Movement = movement;

        _freePointIndex++;
    }

    private void OnUpdate()
    {
        if (BattleController.Instance.IsMoving)
        {
            var enemies = EntityController.Instance.Enemies;

            foreach (var view in EntityController.Instance.Soldiers)
            {
                Vector3 currentPosition = view.transform.position;
                
                AbstractEntity nearestAlly = null;
                float minDistanceSqr = 1000;

                foreach (var enemy in enemies)
                {
                    if (enemy == null) continue;

                    float distanceSqr = (currentPosition - enemy.transform.position).sqrMagnitude;
                    if (distanceSqr < minDistanceSqr)
                    {
                        minDistanceSqr = distanceSqr;
                        nearestAlly = enemy;
                    }
                }

                if (nearestAlly != null)
                {
                    IMovement movement = new ToPointMovement(nearestAlly.transform);
                    
                    if (minDistanceSqr < 3)
                    {
                        AttackController.Instance.UpdateAttack(view, new MiddleAttack(() => Window.Damage, nearestAlly));
                    }
                    
                    UpdateMovement(view, movement);
                    UpdateRotation(view, new LookAtModel(() => view.Child, nearestAlly.transform));
                }

            }

        }
        else
        {
            foreach (var soldier in EntityController.Instance.Soldiers)
            {
                IMovement movement = _soldiers[soldier];
                
                UpdateMovement(soldier, movement);
            }
        }
    }
    
    private void UpdateMovement(AbstractEntity entity, IMovement movement) => 
        MovementController.Instance.UpdateMovement(entity, movement);
    
    private void UpdateRotation(AbstractEntity entity, IRotation rotation) => 
        EntityController.Instance.FullEntities[entity].Rotation = rotation;


    public void Exit()
    {
        
    }
}