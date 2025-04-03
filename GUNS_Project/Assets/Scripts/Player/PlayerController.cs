using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("PlayerController not found!");
                }
            }

            return _instance;
        }
    }

    public Transform Player { get; set; }

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