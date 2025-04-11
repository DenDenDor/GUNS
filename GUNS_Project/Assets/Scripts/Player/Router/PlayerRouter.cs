using System.Collections;
using UnityEngine;

public class PlayerRouter : IRouter
{
    private PlayerWindow Window => UiController.Instance.GetWindow<PlayerWindow>();
    
    private PlayerView _view;
    
    private IMovement _movement;

    private PlayerModel _model;
    
    public void Init()
    {
        PlayerView prefab = Resources.Load<PlayerView>("Prefabs/Player");

        _model = new PlayerModel();
        
        
        _view  = Window.CreatePlayer(prefab, (player) =>
        {
            _model.Movement = new ToCursorMovement(() => Window.Speed, player.transform);
            _model.Rotation = new RotateForwardModel(() => Window.RotationSpeed, () => player.Child);
        }, 
        _model);
    }
    
    public void Exit()
    {
    }
}