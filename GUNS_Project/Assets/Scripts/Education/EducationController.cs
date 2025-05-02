using UnityEngine;
using System;

public class EducationController : MonoBehaviour
{
    private static EducationController _instance;

    public static EducationController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EducationController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("EducationController not found!");
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