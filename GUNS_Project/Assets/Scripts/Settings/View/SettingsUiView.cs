using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUiView : MonoBehaviour
{
    [SerializeField] private Button _close;

    public event Action Closed;
    
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
