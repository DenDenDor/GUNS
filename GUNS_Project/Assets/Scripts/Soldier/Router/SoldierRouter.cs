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

            SoldierView soldier = Window.CreateSolider(_prefab);
        
            _pointsBySoldiers.Add(soldier, Window.MoveToPoints[freePointIndex]);
            IMovement movement = new ToPointMovement(Window.MoveToPoints[freePointIndex]);
            _soldiers.Add(soldier, movement);
        
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnUpdate()
    {
        foreach (var soldier in _soldiers)
        {
            soldier.Key.MoveTo(soldier.Value.GetPosition());
        }
    }

    public void Exit()
    {
    }
}