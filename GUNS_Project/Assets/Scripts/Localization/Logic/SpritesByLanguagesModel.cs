using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(fileName = "SpritesByLanguagesModel", menuName = "Localization/SpritesByLanguagesModel")]
    public class SpritesByLanguagesModel : ScriptableObject
    {
        [SerializeField] private string _key;

        [SerializeField] private SerializedDictionary<LanguageType, Sprite> _spritesByLanguages;
        
        public string Key => _key;

        public SerializedDictionary<LanguageType, Sprite> SpritesByLanguages => _spritesByLanguages;
    }
    
    [Serializable]
    public class SerializedDictionary<TKey, TValue>
    {
        [SerializeField] private List<KeyValue<TKey, TValue>> _items;

        public IEnumerable<TKey> Keys => _items.Select(x => x.Key);

        public IEnumerable<TValue> Values => _items.Select(x => x.Value);

        public TValue this[TKey key] => _items.First(x => x.Key.Equals(key)).Value;

        public void Init()
        {
            _items = new();
        }
        
        public void Add(TKey key, TValue value)
        {
            KeyValue<TKey, TValue> pair = new KeyValue<TKey, TValue>();
            pair.Key = key;
            pair.Value = value;
            
            _items.Add(pair);
        }
        
        public Dictionary<TKey, TValue> ConvertToDictionary()
        {
            var dictionary = new Dictionary<TKey, TValue>();

            foreach (var item in _items)
            {
                dictionary.Add(item.Key, item.Value);
            }

            return dictionary;
        }
    }
    
    [Serializable]
    public class KeyValue<TKey, TValue>
    {
        [SerializeField] private TKey _key;

        [SerializeField] private TValue _value;

        public TKey Key
        {
            get => _key;
            set => _key = value;
        }

        public TValue Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
