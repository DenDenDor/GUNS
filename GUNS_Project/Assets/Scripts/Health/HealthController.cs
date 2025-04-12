using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class HealthController : MonoBehaviour
{
    private Dictionary<AbstractEntity, HealthModel> _entitiesByHealth = new();

    private static HealthController _instance;

    public static HealthController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HealthController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("HealthController not found!");
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

    public AbstractEntity GetByHealth(HealthModel obj)
    {
        AbstractEntity entity = _entitiesByHealth.FirstOrDefault(x => x.Value == obj).Key;

        return entity;
    } 
    
    public HealthModel GetByEntity(AbstractEntity entity)
    {
        return _entitiesByHealth[entity];
    }

    public void Add(AbstractEntity abstractEntity, HealthModel model)
    {
        _entitiesByHealth.Add(abstractEntity, model);
    }
}