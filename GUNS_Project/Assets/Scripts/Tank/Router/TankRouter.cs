using System.Collections.Generic;
using UnityEngine;

public class TankRouter : IRouter
{
    private readonly Dictionary<TankView, IMovement> _tanks = new();
    private int _freePointIndex;
    private TankView _prefab;

    private TankWindow Window => UiController.Instance.GetWindow<TankWindow>();

    private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;
    
    public void Init()
    {
        _prefab = FactoryController.Instance.FindPrefab<TankView>();

        BarrackController.Instance.CreatedTank += OnCreated;
        BattleController.Instance.Restarted += OnRestarted;
        
        UpdateController.Instance.Add(OnUpdate);
    }
    
    private void OnRestarted()
    {
        _freePointIndex = 0;
    }

    private void OnCreated(Transform point)
    {
        List<Transform> points = AllyPoint.MoveToTanks;
        
        if (_freePointIndex >= points.Count || BattleController.Instance.IsMoving || points.Count == EntityController.Instance.Tanks.Count)
        {
            return;
        }
        
        TankModel model = new TankModel();

        TankView soldier = Window.CreateSolider(_prefab, point, model);

        IMovement movement = new ToPointMovement(points[_freePointIndex]);

        _tanks.Add(soldier, movement);

        model.Movement = movement;

        _freePointIndex++;
    }

    private void OnUpdate()
    {
        if (BattleController.Instance.IsMoving)
        {
            var enemies = EntityController.Instance.Enemies;

            foreach (var view in EntityController.Instance.Tanks)
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
                    
                    if (minDistanceSqr < 16)
                    {
                        AttackController.Instance.UpdateAttack(view, new ShootAttack(() => Window.Damage, () => Window.BulletSpeed, view, nearestAlly));
                        UpdateMovement(view, new ToPointMovement(view.transform));
                    }
                    else
                    {
                        UpdateMovement(view, new ToPointMovement(nearestAlly.transform));
                    }
                    
                    UpdateRotation(view, new LookAtModel(() => view.Child, nearestAlly.transform));
                }

            }

        }
        else
        {
            foreach (var soldier in EntityController.Instance.Tanks)
            {
                IMovement movement = _tanks[soldier];
                
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