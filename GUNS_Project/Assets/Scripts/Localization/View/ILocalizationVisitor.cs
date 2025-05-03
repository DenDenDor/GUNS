using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    public interface ILocalizationVisitor
    {
        public void Visit(DisplayLocalizedSprite display);
        public void Visit(DisplayLocalizedText display);
        public void Visit(DisplayLocalizedString display);
        public void Visit(DisplayLocalizedMaterialText display);
    }

    public class DisplayLocalizationViewVisitor : ILocalizationVisitor
    {
        private readonly SpriteDataVisitor _spriteDataVisitor = new();

        private readonly TextDataVisitor _textDataVisitor = new();

        private readonly ILocalizedData _localizedData;


        public DisplayLocalizationViewVisitor(ILocalizedData localizedObject)
        {
            _localizedData = localizedObject;
        }

        public void Visit(DisplayLocalizedText display)
        {
            _localizedData.Accept(_textDataVisitor);
            display.Display(_textDataVisitor.TextData.Text);
        }

        public void Visit(DisplayLocalizedSprite display)
        {
            _localizedData.Accept(_spriteDataVisitor);
            display.Display(_spriteDataVisitor.SpriteData.Sprite);
        }

        public void Visit(DisplayLocalizedString display)
        {
            _localizedData.Accept(_textDataVisitor);
            display.Display(_textDataVisitor.TextData.Text);
        }

        public void Visit(DisplayLocalizedMaterialText display)
        {
            _localizedData.Accept(_textDataVisitor);
            display.Display(_textDataVisitor.TextData.Text);
        }
    }
}