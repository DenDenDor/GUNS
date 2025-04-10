using System.Collections;
using UnityEngine;

public class PlayerRouter : IRouter
{
    private PlayerWindow Window => UiController.Instance.GetWindow<PlayerWindow>();
    
    private PlayerView _view;
    
    private IMovement _movement;
    
    public void Init()
    {
        PlayerView prefab = Resources.Load<PlayerView>("Prefabs/Player");

        PlayerModel model = new PlayerModel();
        
        
        _view  = Window.CreatePlayer(prefab, (player) =>
        {
            model.Movement = new ToCursorMovement(() => Window.Speed, player.transform);
        }, 
            model);
    }
    
    public void Exit()
    {
    }
}