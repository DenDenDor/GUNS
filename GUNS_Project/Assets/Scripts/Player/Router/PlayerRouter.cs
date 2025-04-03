using System.Collections;
using UnityEngine;

public class PlayerRouter : IRouter
{
    private PlayerWindow Window => UiController.Instance.GetWindow<PlayerWindow>();
    
    private PlayerView _view;

    private Coroutine _coroutine;

    private IMovement _movement;
    
    public void Init()
    {
        PlayerView playerView = Resources.Load<PlayerView>("Prefabs/Player");

        _view  = Window.CreatePlayer(playerView);
        _movement = new ToCursorMovement(4, _view.transform);

        _coroutine = Window.StartCoroutine(Update());
    }

    private IEnumerator Update()
    {
        while (true)
        {
            _view.MoveTo(_movement.GetPosition());

            yield return null;
        }
    }

    public void Exit()
    {
        Window.StopCoroutine(_coroutine);
    }
}