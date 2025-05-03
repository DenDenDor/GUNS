using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    [CreateAssetMenu(fileName = "FactoryLocalizedObject", menuName = "Localization/FactoryLocalizedObject")]
    public class FactoryLocalizedObject : ScriptableObject
    {
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private List<SpritesByLanguagesModel> _collectionSpritesByLanguages;

        private readonly CsvParser<LocalizedText> _csvParser = new LocalizedTextCsvParser();

        public IEnumerable<ILocalizedObject<ILocalizedData>> Create()
        {
            IEnumerable<LocalizedText> localizedTexts = _csvParser.Parse(_textAsset);
            GeneratorLocalizedSprites generatorLocalizedSprites =
                new GeneratorLocalizedSprites(_collectionSpritesByLanguages);

            List<ILocalizedObject<ILocalizedData>> localizedObjects = new List<ILocalizedObject<ILocalizedData>>();

            foreach (var sprite in generatorLocalizedSprites.Generate())
                localizedObjects.Add(sprite);

            foreach (var text in localizedTexts)
                localizedObjects.Add(text);

            return localizedObjects;
        }
    }
}