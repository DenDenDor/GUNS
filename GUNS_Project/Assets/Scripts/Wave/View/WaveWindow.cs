using System.Collections.Generic;
using UnityEngine;

public class WaveWindow : AbstractWindowUi
{
    private IEnumerable<AbstractWaveInfo> _waves;

    public IEnumerable<AbstractWaveInfo> Waves => _waves;

    public override void Init()
    {
        AbstractWaveInfo[] waveInfos = gameObject.GetComponentsInChildren<AbstractWaveInfo>();

        for (int i = 0; i < waveInfos.Length; i++)
        {
            waveInfos[i].IdLevel = i;
        }

        _waves = waveInfos;
    }
}

