using UnityEngine;

public class SettingsWindow : AbstractFactoryWindow
{
    [SerializeField] private Transform _pointPanel;
    [SerializeField] private Transform _pointButton;
    
    public override void Init()
    {
        
    }

    public SettingsUiView CreatePanel(SettingsUiView findPrefab)
    {
        return CreatePrefab(findPrefab, _pointPanel, true);
    }   
    
    public SettingsButtonUi CreateButton(SettingsButtonUi button)
    {
        return CreatePrefab(button, _pointButton, true);
    }
}
