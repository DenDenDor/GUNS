using UnityEngine;
using System;

public class BattleController : MonoBehaviour
{
    private bool _isMoving;

    public bool IsMoving => _isMoving;

    private static BattleController _instance;

    public static BattleController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BattleController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("BattleController not found!");
                }
            }

            return _instance;
        }
    }

    public event Action Restarted;

    public event Action StartedBattle;
    public event Action EndedBattle;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void Restart()
    {
        Restarted?.Invoke();
    }

    public void StartMoving()
    {
        _isMoving = true;
        StartedBattle?.Invoke();
    }

    public void StopMoving()
    {
        _isMoving = false;
        EndedBattle?.Invoke();
    }
}