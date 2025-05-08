using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
    private static LevelController _instance;

    public static LevelController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("LevelController not found!");
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

    public event Action Updated;

    public int Level { get; private set; }

    public void UpdateLevel(int level)
    {
        Level = level;
        Updated?.Invoke();
    }
}