using System.Collections;
using UnityEngine;

public class PlayerRouter : IRouter
{
    private PlayerWindow Window => UiController.Instance.GetWindow<PlayerWindow>();
    
    private PlayerView _view;
    
    private IMovement _movement;

    private PlayerModel _model;
    
    private Vector3 _previousPosition;

    public void Init()
    {
        PlayerView prefab = Resources.Load<PlayerView>("Prefabs/Player");

        _model = new PlayerModel();
        
        
        _view  = Window.CreatePlayer(prefab, (player) =>
        {
            _model.Movement = new ToCursorMovement(() => Window.Speed, player.transform);
            _previousPosition = player.transform.position;
        }, 
        _model);
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        Vector3 direction = _previousPosition - _view.transform.position;
        
        if (direction.magnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

             _view.RotateTo(Quaternion.Slerp(
                 _view.Child.rotation, 
                 targetRotation, 
                 Window.RotationSpeed * Time.deltaTime));
        }
        
        _previousPosition = _view.transform.position;
    }
    
    public void Exit()
    {
    }
}