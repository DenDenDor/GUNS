using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "UpgradeStatSO", menuName = "Scriptable Objects/UpgradeStatSO")]
public class UpgradeStatSO : ScriptableObject
{
    [SerializeField] private SerializedDictionary<UpgradeType, Sprite> _iconsByUpgradesType;
    
    [SerializeField] private SerializedDictionary<int, float> _health;
    [SerializeField] private SerializedDictionary<int, float> _speed;
    [SerializeField] private SerializedDictionary<int, float> _damage;

    public Sprite GetUpgradeSprite(UpgradeType type)
    {
        return _iconsByUpgradesType[type];
    }

    public bool TryGetIndexForLevel(UpgradeType type, float value, out int index)
    {
        SerializedDictionary<int, float> dictionary = GetDictionaryByType(type);
        index = -1;

        if (dictionary == null || dictionary.ConvertToDictionary().Count == 0)
        {
            return false;
        }

        var sortedEntries = dictionary.ConvertToDictionary()
            .OrderBy(x => x.Key)
            .ToList();

        for (int i = 0; i < sortedEntries.Count; i++)
        {
            if (Mathf.Approximately(sortedEntries[i].Value, value))
            {
                index = i;
                return true;
            }
        }
        
        return false;
    }
    
    public bool TryGetValueForLevel(UpgradeType type, int level, out float value)
    {
        SerializedDictionary<int, float> dictionary = GetDictionaryByType(type);
        
        if (dictionary == null || dictionary.ConvertToDictionary().Count == 0)
        {
            value = default;
            return false;
        }

        int closestKey = -1;
        foreach (var key in dictionary.Keys)
        {
            if (key <= level && key > closestKey)
            {
                closestKey = key;
            }
        }

        if (closestKey != -1)
        {
            value = dictionary[closestKey];
            return true;
        }

        value = default;
        return false;
    }

    private SerializedDictionary<int, float> GetDictionaryByType(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Health => _health,
            UpgradeType.Speed => _speed,
            UpgradeType.Damage => _damage,
            _ => null
        };
    }
    
    public bool TryGetLevel(UpgradeType type, float currentValue, out int previousLevel, out int nextLevel)
    {
        var dictionary = GetDictionaryByType(type);
        previousLevel = -1;
        nextLevel = -1;

        if (dictionary == null || dictionary.ConvertToDictionary().Count == 0)
            return false;

        var sortedLevels = new List<int>(dictionary.Keys);
        sortedLevels.Sort();

        for (int i = 0; i < sortedLevels.Count; i++)
        {
            int level = sortedLevels[i];
            if (level <= currentValue)
            {
                previousLevel = level;
            }
            else
            {
                nextLevel = level;
                break;
            }
        }
        
        return previousLevel != -1 || nextLevel != -1;
    }
}