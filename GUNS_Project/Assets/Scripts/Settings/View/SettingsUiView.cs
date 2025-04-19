using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUiView : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private IconButtonUi _sound;
    [SerializeField] private IconButtonUi _music;

    public IconButtonUi Sound => _sound;

    public IconButtonUi Music => _music;

    public event Action Closed;
    public event Action ClickedSound;
    public event Action ClickedMusic;
    
    private void Awake()
    {
        _close.onClick.AddListener(() => Closed?.Invoke());
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }  
    
    public void Open()
    {
        gameObject.SetActive(true);
    }
}
