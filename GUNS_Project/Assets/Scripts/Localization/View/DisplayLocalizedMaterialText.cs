using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Localization
{

    public class DisplayLocalizedMaterialText : DisplayView
    {
        [SerializeField] private string _key;

        [SerializeField] private TextMeshPro _text;

        public override string Key => _key.Trim();

        public void Display(string text) => _text.text = text;

        public override void Accept(ILocalizationVisitor visitor) => visitor.Visit(this);
    }
}