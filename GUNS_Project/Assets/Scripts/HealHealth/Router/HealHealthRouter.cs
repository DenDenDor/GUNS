using UnityEngine;

public class HealHealthRouter : IRouter
{
    private HealHealthWindow Window => UiController.Instance.GetWindow<HealHealthWindow>();

    private HealHealthView _view;
    
    public void Init()
    {
        HealthController.Instance.LowHealthPlayer += OnShowLowHealthPlayer;
    }

    private void OnShowLowHealthPlayer()
    {
        UpdateController.Instance.StopTime();

        _view = Window.Create(FactoryController.Instance.FindPrefab<HealHealthView>());

        _view.Exited += OnClose;
        _view.Accepted += OnAccept;
    }

    private void OnClose()
    {
        Object.Destroy(_view.gameObject);
        
        UpdateController.Instance.ContinueTime();
    }

    private void OnAccept()
    {
        HealthController.Instance.Heal(EntityController.Instance.Player);
        OnClose();
    }

    public void Exit()
    {
        HealthController.Instance.LowHealthPlayer -= OnShowLowHealthPlayer;
    }
}