using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if DOTWEEN
using DG.Tweening;
#endif

namespace Localization
{

    public class ChangeLanguageButton : MonoBehaviour
    {
        [SerializeField] private ButtonUi _buttonUi;

        private void Start()
        {
            _buttonUi.Clicked += OnClick;
        }

        private void OnClick()
        {
            LocalizedInstaller.Instance.ChangeLanguage();
        }
    }
    
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

}