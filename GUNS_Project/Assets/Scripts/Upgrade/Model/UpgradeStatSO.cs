using UnityEngine;
using System.Collections.Generic;

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
    
    public bool TryGetNextLevel(UpgradeType type, float currentValue, out int previousLevel, out int nextLevel)
    {
        var dictionary = GetDictionaryByType(type);
        previousLevel = -1;
        nextLevel = -1;

        if (dictionary == null || dictionary.ConvertToDictionary().Count == 0)
            return false;

        // Получаем все уровни и сортируем их по возрастанию
        var sortedLevels = new List<int>(dictionary.Keys);
        sortedLevels.Sort();

        // Находим предыдущий уровень (максимальный уровень, который ≤ currentValue)
        for (int i = 0; i < sortedLevels.Count; i++)
        {
            int level = sortedLevels[i];
            if (level <= currentValue)
            {
                previousLevel = level;
            }
            else
            {
                // Нашли первый уровень, который > currentValue - это наш nextLevel
                nextLevel = level;
                break;
            }
        }

        // Если все уровни ≤ currentValue, nextLevel останется -1
        // Если currentValue меньше всех уровней, previousLevel останется -1
    
        return previousLevel != -1 || nextLevel != -1;
    }
}