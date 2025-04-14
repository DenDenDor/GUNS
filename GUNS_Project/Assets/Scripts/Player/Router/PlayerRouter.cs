using System.Collections;
using UnityEngine;

public class PlayerRouter : IRouter
{
    private PlayerWindow Window => UiController.Instance.GetWindow<PlayerWindow>();
    
    private PlayerView _view;
    
    private IMovement _movement;

    private PlayerModel _model;
    private PlayerView _prefab;
    
    public void Init()
    {
        _prefab = Resources.Load<PlayerView>("Prefabs/Player");
        
        WaveController.Instance.StartedNewWave += OnStartNewWave;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        var enemies = EntityController.Instance.Enemies;

        if (_view == null)
        {
            return;
        }
        
        Vector3 currentPosition = _view.transform.position;
                
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
            if (minDistanceSqr < 25)
            {
                Debug.Log("SHOOT!!!");
                AttackController.Instance.UpdateAttack(_view, new ShootAttack(() => Window.Damage, () => Window.BulletSpeed, _view, nearestAlly));
            }
        }
    }

    private void OnStartNewWave()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        _model = new PlayerModel();

        _view  = Window.CreatePlayer(_prefab, GeneratePlayerModel, _model);
    }

    private void GeneratePlayerModel(PlayerView player)
    {
        _model.Movement = new ToCursorMovement(() => Window.Speed, player.transform);
        _model.Rotation = new RotateForwardModel(() => Window.RotationSpeed, () => player.Child);
    }

    public void Exit()
    {
    }
}