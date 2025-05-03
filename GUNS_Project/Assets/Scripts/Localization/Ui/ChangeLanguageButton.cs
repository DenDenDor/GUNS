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
}