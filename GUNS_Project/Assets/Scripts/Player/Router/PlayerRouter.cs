using System.Collections;
using UnityEngine;

public class PlayerRouter : IRouter
{
    private PlayerWindow Window => UiController.Instance.GetWindow<PlayerWindow>();
    
    private PlayerView _view;
    
    private IMovement _movement;
    
    public void Init()
    {
        PlayerView playerView = Resources.Load<PlayerView>("Prefabs/Player");

        _view  = Window.CreatePlayer(playerView);
        _movement = new ToCursorMovement(4, _view.transform);
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        _view.MoveTo(_movement.GetPosition());
    }

    public void Exit()
    {
    }
}