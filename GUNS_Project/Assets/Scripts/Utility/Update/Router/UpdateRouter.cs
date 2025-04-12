using System.Collections;
using UnityEngine;

public class UpdateRouter : IRouter
{
    private UpdateWindow Window => UiController.Instance.GetWindow<UpdateWindow>();

    private Coroutine _coroutine;

    public void Init()
    {
        _coroutine = Window.StartCoroutine(Update());
    }
    
    private IEnumerator Update()
    {
        while (true)
        {
            UpdateController.Instance.UpdateActions();

            yield return null;
        }
    }

    public void Exit()
    {
        Window.StopCoroutine(_coroutine);
    }
}