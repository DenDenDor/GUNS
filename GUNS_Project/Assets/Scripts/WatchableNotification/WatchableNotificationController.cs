using UnityEngine;
using System;

public class WatchableNotificationController : MonoBehaviour
{
    private static WatchableNotificationController _instance;

    public static WatchableNotificationController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WatchableNotificationController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("WatchableNotificationController not found!");
                }
            }

            return _instance;
        }
    }

    public event Action<int> GetGold;
    
    public event Action<int> GetSilver; 

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }
    
  //  public void Update
}