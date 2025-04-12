using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UpgradeStatSO", menuName = "Scriptable Objects/UpgradeStatSO")]
public class UpgradeStatSO : ScriptableObject
{
    [SerializeField] private SerializedDictionary<int, float> _health;
    [SerializeField] private SerializedDictionary<int, float> _speed;
    [SerializeField] private SerializedDictionary<int, float> _damage;

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
}