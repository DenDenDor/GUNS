using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveWindow : AbstractFactoryWindow
{
    private IEnumerable<AbstractWaveInfo> _waves;

    public IEnumerable<AbstractWaveInfo> Waves => _waves;
    public FlagView Flag { get; private set; }

    public event Action CreatedFlag;

    public override void Init()
    {
        AbstractWaveInfo[] waveInfos = gameObject.GetComponentsInChildren<AbstractWaveInfo>();

        for (int i = 0; i < waveInfos.Length; i++)
        {
            waveInfos[i].IdLevel = i + 1;
        }

        _waves = waveInfos;
    }

    public FlagView Create(FlagView prefab, Vector3 enemyPosition)
    {
        FlagView created = CreatePrefab(prefab, enemyPosition);
        Flag = created;
        CreatedFlag?.Invoke();

        return created;
    }
}

