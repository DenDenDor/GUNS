using UnityEngine;
using System;

public class WaveController : MonoBehaviour
{
    public event Action Updated;
    
    private AbstractWaveInfo _abstractWaveInfo;
    
    private static WaveController _instance;

    public static WaveController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WaveController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("WaveController not found!");
                }
            }

            return _instance;
        }
    }
    
    public AbstractWaveInfo GenerateWaveInfo() => _abstractWaveInfo;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void UpdateWave(AbstractWaveInfo waveInfo)
    {
        _abstractWaveInfo = waveInfo;
        Updated?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpdateWave(null);
        }
    }
}