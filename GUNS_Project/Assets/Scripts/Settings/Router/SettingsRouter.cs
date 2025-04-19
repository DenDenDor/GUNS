using UnityEngine;

public class SettingsRouter : IRouter
{
    private SettingsUiView _panel;
    private SettingsButtonUi _button;

    private IconButtonUi Sound => _panel.Sound;
    private IconButtonUi Music => _panel.Music;
    private bool IsSoundTurnOn => SDKMediator.Instance.GenerateSaveData().IsSoundTurnOn;
    private bool IsMusicTurnOn => SDKMediator.Instance.GenerateSaveData().IsMusicTurnOn;
    
    
    public void Init()
    {
        _button = UiController.Instance.GetWindow<SettingsWindow>().CreateButton(FactoryController.Instance.FindPrefab<SettingsButtonUi>());
        
        _panel = UiController.Instance.GetWindow<SettingsWindow>().CreatePanel(FactoryController.Instance.FindPrefab<SettingsUiView>());

        _button.Opened += OnOpened;
        
        _panel.Closed += OnClosed;
        
        UpdateSoundSprite();
        UpdateMusicSprite();
        
        Music.Clicked += OnMusicClicked;
        Sound.Clicked += OnSoundClicked;
        
        _panel.Close();
    }

    private void UpdateSoundSprite()
    {
        if (IsSoundTurnOn)
        {
            Sound.UpdateActivateSprite();
        }
        else
        {
            Sound.UpdateDisableSprite();
        }
    }

    private void UpdateMusicSprite()
    {
        if (IsMusicTurnOn)
        {
            Music.UpdateActivateSprite();
        }
        else
        {
            Music.UpdateDisableSprite();
        }
    }

    private void OnSoundClicked()
    {
        bool isValue = IsSoundTurnOn == false;
        
        SDKMediator.Instance.SaveIsSoundTurnOn(isValue);

        UpdateSoundSprite();
    }

    private void OnMusicClicked()
    {
        bool isValue = IsMusicTurnOn == false;
        
        SDKMediator.Instance.SaveIsMusicTurnOn(isValue);

        UpdateMusicSprite();
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
