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


    private AbstractPressurePlateView Plate =>
        PressurePlateController.Instance.PressurePlateViewsByPoints[Window.SoldierAttackButton];

    public void Init()
    {
        _prefab = Resources.Load<SoldierView>("Prefabs/Soldier");
        
        BarrackController.Instance.Created += OnCreated;
        BattleController.Instance.Restarted += OnRestarted;
        
        UpdateController.Instance.Add(OnUpdate);
        
        PressurePlateController.Instance.AddPressurePlate(Window.SoldierAttackButton, PressurePlateType.FillingUp);
        
        Plate.UpdateBar(0);

        Plate.Entered += OnEntered;
        Plate.Exited += OnExited;
    }

    private void OnRestarted()
    {
        _isMoving = false;
        _freePointIndex = 0;
        Plate.UpdateBar(0);
    }

    private void OnCreated(Transform point)
    {
        if (_freePointIndex >= Window.MoveToPoints.Count || _isMoving)
        {
            return;
        }
        
        SoldierModel model = new SoldierModel();

        SoldierView soldier = Window.CreateSolider(_prefab, point, model);

        IMovement movement = new ToPointMovement(Window.MoveToPoints[_freePointIndex]);

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
                        AttackController.Instance.UpdateAttack(view, new MiddleAttack(2, nearestAlly));
                    }
                    
                    UpdateMovement(view, movement);
                }

            }

        }
        else
        {
            foreach (var soldier in EntityController.Instance.Soldiers)
            {
                UpdateMovement(soldier, _soldiers[soldier]);
            }
        }
    }
    
    private void UpdateMovement(AbstractEntity entity, IMovement movement)
    {
        MovementController.Instance.UpdateMovement(entity, movement);
    }

    public void Exit()
    {
        Plate.Entered -= OnEntered;
    }
}