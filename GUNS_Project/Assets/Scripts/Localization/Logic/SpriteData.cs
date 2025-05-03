using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    public struct SpriteData : ILocalizedData
    {
        public SpriteData(LanguageType languageType, Sprite sprite)
        {
            LanguageType = languageType;
            Sprite = sprite;
        }

        public LanguageType LanguageType { get; }

        public Sprite Sprite { get; }

        public void Accept(ILocalizedDataVisitor visitor) => visitor.Visit(this);
    }

    public interface ILocalizedData
    {
        public LanguageType LanguageType { get; }

        public void Accept(ILocalizedDataVisitor visitor);
    }

    public struct TextData : ILocalizedData
    {
        public TextData(LanguageType languageType, string text)
        {
            LanguageType = languageType;
            Text = text;
        }

        public LanguageType LanguageType { get; }

        public string Text { get; }

        public void Accept(ILocalizedDataVisitor visitor) => visitor.Visit(this);
    }
}