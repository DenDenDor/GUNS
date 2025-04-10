using UnityEngine;
using System;

public class WaveController : MonoBehaviour
{
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

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }
}