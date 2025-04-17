using UnityEngine;

public class SettingsRouter : IRouter
{
    private SettingsUiView _panel;
    private SettingsButtonUi _button;
    
    public void Init()
    {
        _button = UiController.Instance.GetWindow<SettingsWindow>().CreateButton(FactoryController.Instance.FindPrefab<SettingsButtonUi>());
        
        _panel = UiController.Instance.GetWindow<SettingsWindow>().CreatePanel(FactoryController.Instance.FindPrefab<SettingsUiView>());

        _button.Opened += OnOpened;
        
        _panel.Closed += OnClosed;

        _panel.Close();
    }

    private void OnOpened()
    {
        _panel.Open();
    }
    
    private void OnClosed()
    {
        _panel.Close();
    }

    public void Exit()
    {
        
    }
}
