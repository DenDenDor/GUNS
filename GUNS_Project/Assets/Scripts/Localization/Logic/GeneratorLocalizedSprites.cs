using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    public class GeneratorLocalizedSprites
    {
        private readonly IEnumerable<SpritesByLanguagesModel> _collection;

        public GeneratorLocalizedSprites(IEnumerable<SpritesByLanguagesModel> collection)
        {
            _collection = collection;
        }

        public IEnumerable<LocalizedSprite> Generate()
        {
            List<LocalizedSprite> localizedSprites = new List<LocalizedSprite>();

            foreach (var spritesByLanguages in _collection)
            {
                List<ILocalizedData> listByLanguages = new List<ILocalizedData>();

                foreach (var element in spritesByLanguages.SpritesByLanguages.ConvertToDictionary())
                {
                    listByLanguages.Add(new SpriteData(element.Key, element.Value));
                }

                localizedSprites.Add(new LocalizedSprite(spritesByLanguages.Key, listByLanguages));
            }

            return localizedSprites;
        }

    }

    public class LocalizedSprite : ILocalizedObject<ILocalizedData>
    {
        public LocalizedSprite(string key, IReadOnlyList<ILocalizedData> objectByLanguage)
        {
            Key = key;
            ObjectByLanguage = objectByLanguage;
        }

        public string Key { get; private set; }

        public IReadOnlyList<ILocalizedData> ObjectByLanguage { get; private set; }

    }
}