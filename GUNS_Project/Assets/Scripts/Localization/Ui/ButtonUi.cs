using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUi : MonoBehaviour
{
    private Button _button;
    private Vector3 _originalScale;

    public event Action Clicked;
    
    private void Start()
    {
        _button = GetComponent<Button>();

        _originalScale = transform.localScale;

        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Clicked?.Invoke();
        
#if DOTWEEN
            transform.DOScale(_originalScale * 1.1f, 0.2f) 
                .OnComplete(() =>
                {
                    transform.DOScale(_originalScale, 0.2f); 
                });
#endif

    }

}