using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{

    public class DisplayLocalizedSprite : DisplayView
    {
        [SerializeField] private string _key;

        [SerializeField] private Image _image;

        public override string Key => _key;

        public void Display(Sprite sprite)
        {
            if (_image == null)
                return;

            _image.sprite = sprite;
        }

        public override void Accept(ILocalizationVisitor visitor) => visitor.Visit(this);

    }
}