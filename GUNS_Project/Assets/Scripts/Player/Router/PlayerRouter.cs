using System.Collections;
using UnityEngine;

public class PlayerRouter : IRouter
{
    private PlayerWindow Window => UiController.Instance.GetWindow<PlayerWindow>();
    
    private PlayerView _view;
    
    private IMovement _movement;

    private PlayerModel _model;
    private PlayerView _prefab;

    private IRotation _previousModel;
    
    public void Init()
    {
        _prefab = Resources.Load<PlayerView>("Prefabs/Player");
        
        WaveController.Instance.StartedNewWave += OnStartNewWave;
        
        UpdateController.Instance.Add(OnUpdate);
        
        MovementController.Instance.StartedMoving += OnStartedMoving; 
        MovementController.Instance.StoppedMoving += OnStopMoving; 
    }

    private void OnStopMoving()
    {
        MovementController.Instance.UpdateMovement(_view, null);
    }

    private void OnStartedMoving()
    {
        MovementController.Instance.UpdateMovement(_view, new ToCursorMovement(() => Window.Speed, _view.transform));
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

        bool isFound = false;

        if (nearestAlly != null)
        {
            if (minDistanceSqr < 25)
            {
                Debug.Log("SHOOT!!!");
                AttackController.Instance.UpdateAttack(_view, new ShootAttack(() => Window.Damage, () => Window.BulletSpeed, _view, nearestAlly));
                UpdateRotation(_view, new LookAtModel(() => _view.Child, nearestAlly.transform));
                isFound = true;
            }
            
            Debug.Log("NEAREST ALLY FOUND!");
        }

        if (isFound == false && _previousModel is not RotateForwardModel)
        {
            UpdateRotation(_view, new RotateForwardModel(() => Window.RotationSpeed, () => _view.Child));
            Debug.Log("RotateForwardModel!");
        }
    }

    private void OnStartNewWave()
    {
        _previousModel = null;
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
      //  _model.Rotation = new RotateForwardModel(() => Window.RotationSpeed, () => _view.Child);

        //UpdateRotation(player, new RotateForwardModel(() => Window.RotationSpeed, () => _view.Child));
    }

    private void UpdateRotation(AbstractEntity entity, IRotation rotation)
    {
        _previousModel = rotation;
        EntityController.Instance.FullEntities[entity].Rotation = rotation;
    }

    public void Exit()
    {
    }
}