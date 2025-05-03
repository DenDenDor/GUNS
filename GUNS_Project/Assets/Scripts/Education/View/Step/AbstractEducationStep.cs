using System;
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

    public void Close()
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

    public void Init()
    {
        Key = GetComponent<DisplayLocalizedString>();
    }
}
