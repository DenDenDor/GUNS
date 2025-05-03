using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    public interface ILocalizedDataVisitor
    {
        public void Visit(SpriteData data);

        public void Visit(TextData data);
    }

    public class SpriteDataVisitor : ILocalizedDataVisitor
    {
        public SpriteData SpriteData { get; private set; }

        public void Visit(SpriteData data) => SpriteData = data;

        public void Visit(TextData data)
        {
        }
    }

    public class TextDataVisitor : ILocalizedDataVisitor
    {
        public TextData TextData { get; private set; }

        public void Visit(SpriteData data)
        {
        }

        public void Visit(TextData data) => TextData = data;
    }
}