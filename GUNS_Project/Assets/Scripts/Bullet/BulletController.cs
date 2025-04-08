using UnityEngine;
using System;
using System.Collections.Generic;

public class BulletController : MonoBehaviour
{
    private readonly Dictionary<BulletView, BulletModel> _bullets = new();

    public Dictionary<BulletView, BulletModel> Bullets => _bullets;

    private static BulletController _instance;

    public static BulletController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BulletController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("BulletController not found!");
                }
            }

            return _instance;
        }
    }

    public event Action<BulletView> Added;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public event Action<AbstractEntity, AbstractEntity> Created;

    public void Create(AbstractEntity thisEntity, AbstractEntity toAttack)
    {
        Created?.Invoke(thisEntity, toAttack);
    }

    public void Add(BulletView view, BulletModel model)
    {
        _bullets.Add(view, model);
        
        Added?.Invoke(view);
    }
}