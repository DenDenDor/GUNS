using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Localization
{

    public class DisplayLocalizedText : DisplayView
    {
        [SerializeField] private string _key;

        [SerializeField] private TextMeshProUGUI _text;

        private readonly int _maxCharactersPerLine = 35;

        public override string Key => _key.Trim();

        public void Display(string text)
        {
            _text.text = text.Trim('"');
        }

        public override void Accept(ILocalizationVisitor visitor) => visitor.Visit(this);

        public void Init(string text)
        {
            _key = text;
        }
    }
}