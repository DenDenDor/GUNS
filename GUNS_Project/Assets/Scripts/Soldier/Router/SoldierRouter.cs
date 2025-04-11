using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRouter : IRouter
{
    private readonly Dictionary<SoldierView, IMovement> _soldiers = new();
    private Coroutine _coroutine;
    private bool _isMoving;
    private int _freePointIndex;
    private SoldierView _prefab;

    private SoldierWindow Window => UiController.Instance.GetWindow<SoldierWindow>();

    private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;

    private AbstractPressurePlateView Plate =>
        PressurePlateController.Instance.PressurePlateViewsByPoints[AllyPoint.AttackButton];

    public void Init()
    {
        _prefab = Resources.Load<SoldierView>("Prefabs/Soldier");
        
        BarrackController.Instance.Created += OnCreated;
        BattleController.Instance.Restarted += OnRestarted;
        
        UpdateController.Instance.Add(OnUpdate);
        
        WaveController.Instance.StartedNewWave += OnStartNewWave;
    }

    private void SubscribePlate()
    {
        Debug.Log("INVOKE SOLDIER PLATE!!!");
        PressurePlateController.Instance.AddPressurePlate(AllyPoint.AttackButton, PressurePlateType.FillingUp);
        
        Plate.UpdateBar(0);

        Plate.Entered += OnEntered;
        Plate.Exited += OnExited;
    }

    private void OnStartNewWave()
    {
        SubscribePlate();
    }

    private void OnRestarted()
    {
        _isMoving = false;
        _freePointIndex = 0;
        Plate.UpdateBar(0);
    }

    private void OnCreated(Transform point)
    {
        List<Transform> points = AllyPoint.MoveToPoints;
        
        if (_freePointIndex >= points.Count || _isMoving)
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

    private void OnExited()
    {
        if (_isMoving == false)
        {
            CoroutineController.Instance.StopCoroutine(_coroutine);
        
            Plate.UpdateBar(0);
        }
    }

    private IEnumerator Cooldown()
    {
        float fillness = 0;
        float time = 0;
        
        while (time < 1)
        {
            time += Time.deltaTime;
            
            Plate.UpdateBar(time);

            yield return null;
        }
        
        _isMoving = true;
    }

    private void OnEntered(AbstractPressurePlateView view)
    {
        if (_isMoving == false)
        {
            _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown());
        }
    }

    private void OnUpdate()
    {
        if (_isMoving)
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
        Plate.Entered -= OnEntered;
    }
}