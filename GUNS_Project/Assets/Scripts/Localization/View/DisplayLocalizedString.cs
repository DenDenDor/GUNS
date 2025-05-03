using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    public class DisplayLocalizedString : DisplayView
    {
        [SerializeField] private string _key;

        public override string Key => _key;

        public string LocalizedText { get; private set; }

        public event Action<string> UpdatedLocalization;

        public void SetKey(string key)
        {
            _key = key;
        }

        public void Display(string text)
        {
            string newText = text.Trim('"');

            LocalizedText = newText;
            UpdatedLocalization?.Invoke(newText);
        }

        public override void Accept(ILocalizationVisitor visitor) => visitor.Visit(this);
    }
}