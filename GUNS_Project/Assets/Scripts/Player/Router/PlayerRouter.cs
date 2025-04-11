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
        
        CreatePlayer();
        
        WaveController.Instance.StartedNewWave += OnStartNewWave;
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