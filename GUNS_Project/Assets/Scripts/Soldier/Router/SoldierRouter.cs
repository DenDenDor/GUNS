using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRouter : IRouter
{
    private readonly Dictionary<SoldierView, IMovement> _soldiers = new();
    private readonly Dictionary<SoldierView, Transform> _pointsBySoldiers = new();
    
    private SoldierView _prefab;
    
    private SoldierWindow Window => UiController.Instance.GetWindow<SoldierWindow>();
    
    public void Init()
    {
        _prefab = Resources.Load<SoldierView>("Prefabs/Soldier");
        
        UpdateController.Instance.StartCoroutine(Wait());
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private IEnumerator Wait()
    {
        List<bool> occupiedPoints = new List<bool>();
        
        for (int i = 0; i < Window.MoveToPoints.Count; i++)
        {
            occupiedPoints.Add(false);
        }

        for (int i = 0; i < 5; i++)
        {
            int freePointIndex = -1;
            
            for (int j = 0; j < occupiedPoints.Count; j++)
            {
                if (!occupiedPoints[j])
                {
                    freePointIndex = j;
                    occupiedPoints[j] = true;
                    break;
                }
            }

            if (freePointIndex == -1) 
                yield break;

            SoldierModel model = new SoldierModel();

            SoldierView soldier = Window.CreateSolider(_prefab, model);

            IMovement movement = new ToPointMovement(Window.MoveToPoints[freePointIndex]);
        
            _pointsBySoldiers.Add(soldier, Window.MoveToPoints[freePointIndex]);
            _soldiers.Add(soldier, movement);

            model.Movement = movement;
        
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnUpdate()
    {
        if (Input.GetKey(KeyCode.E))
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
    }
}