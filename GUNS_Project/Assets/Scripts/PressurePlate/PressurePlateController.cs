using UnityEngine;
using System;

public class PressurePlateController : MonoBehaviour
{
    private static PressurePlateController _instance;

    public static PressurePlateController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PressurePlateController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("PressurePlateController not found!");
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
    
    //public void AddPres
}