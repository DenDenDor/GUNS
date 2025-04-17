using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonUi : MonoBehaviour
{
    [SerializeField] private Button _opened;
    
    public event Action Opened;
    
    private void Awake()
    {
        _opened.onClick.AddListener(() => Opened?.Invoke());
    }
}
