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

            float distance = Vector3.Distance(view.transform.position, PlayerController.Instance.Player.position);
            
            if (distance < 5)
            {
                model.Movement = new ToPointMovement(PlayerController.Instance.Player);
            }
            else
            {
                model.Movement = new ToPointMovement(model.StartPoint);
            }
            
            view.MoveTo(model.Movement.GetPosition());
        }
    }

    public void Exit()
    {
        
    }
}