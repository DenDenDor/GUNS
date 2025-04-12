using System.Collections.Generic;
using UnityEngine;

public class WaveWindow : AbstractWindowUi
{
    private IEnumerable<AbstractWaveInfo> _waves;

    public IEnumerable<AbstractWaveInfo> Waves => _waves;

    public override void Init()
    {
        _waves = gameObject.GetComponentsInChildren<AbstractWaveInfo>();
    }
}

