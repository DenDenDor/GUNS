using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IconButtonUi : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _disableSprite;
    
    public event Action Clicked;
    
    private void Awake()
    {
        _button.onClick.AddListener(() => Clicked?.Invoke());
    }

    public void UpdateActivateSprite()
    {
        _icon.sprite = _activeSprite;
    }  
    
    public void UpdateDisableSprite()
    {
        _icon.sprite = _disableSprite;
    }
}
