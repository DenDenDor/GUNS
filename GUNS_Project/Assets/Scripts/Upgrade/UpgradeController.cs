using UnityEngine;
using System;
using System.Collections.Generic;

public class UpgradeController : MonoBehaviour
{
    private Dictionary<UpgradeType, int> _upgradeLevels = new();
    
    private UpgradeStatModel _upgradeStat;
    
    private static UpgradeController _instance;

    public static UpgradeController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UpgradeController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("UpgradeController not found!");
                }
            }

            return _instance;
        }
    }

    public UpgradeStatModel Stat => _upgradeStat;

    public event Action Upgraded;
    public event Action StoppedUpgraded;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void SetUpgradeLevel(UpgradeType type, int level)
    {
        _upgradeLevels[type] = level;
    }

    public void UpdateStat(UpgradeStatModel model)
    {
        _upgradeStat = model;
        Upgraded?.Invoke();
    }
    
    public void StopUpgraded()
    {
        StoppedUpgraded?.Invoke();
    }
}