using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class WaveController : MonoBehaviour
{
    public event Action Cleared;
    
    public event Action StartedNewWave;
    
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
        Cleared?.Invoke();

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("STARTED NEW WAVE");
        StartedNewWave?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpdateWave(UiController.Instance.GetWindow<WaveWindow>().Waves.LastOrDefault());
        }
    }
}