using System;
using System.Collections;
using Localization;
using UnityEngine;

public abstract class AbstractEducationStep : MonoBehaviour
{
    [field: SerializeField] public DisplayLocalizedString Key { get; private set; }

    protected LookAtObject LookAtObject => UiController.Instance.GetWindow<PlayerWindow>().LookAtObject;
    protected bool IsWorking { get; private set; }

    public event Action<AbstractEducationStep> Finished;

    public void Open()
    {
        IsWorking = true;
        OnOpen();
    }

    protected void Close()
    {
        Finished?.Invoke(this);
        IsWorking = false;
        OnClose();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnOpen()
    {
        
    }

    protected virtual void OnClose()
    {
        
    }

    protected virtual void OnUpdate()
    {
        
        
        
    }
    private Coroutine _coroutine;

    protected void EnterArrow(Func<Transform> func)
    {
        _coroutine = StartCoroutine(Cooldown(func));
    }

    private IEnumerator Cooldown(Func<Transform> func)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            LookAtObject.Appear(func());

        }
    }

    protected void ExitArrow()
    {
        StopCoroutine(_coroutine);
        
        LookAtObject.Disappear();
    }
    
    public void Init()
    {
        Key = GetComponent<DisplayLocalizedString>();
    }
}
