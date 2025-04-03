using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EntityController : MonoBehaviour
{
    private List<AbstractEntity> _entities = new();
    
    private static EntityController _instance;

    public static EntityController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EntityController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("EntityController not found!");
                }
            }

            return _instance;
        }
    }

    public List<AbstractEntity> Entities => _entities;

    public List<AbstractEntity> AllyEntities
    {
        get
        {
            List<AbstractEntity> entities = new List<AbstractEntity>();
            
            entities.AddRange(Soldiers);
            entities.Add(Player);

            return entities;
        }
    }

    public List<EnemyView> Enemies => Get<EnemyView>();

    public List<SoldierView> Soldiers => Get<SoldierView>();

    public PlayerView Player => Get<PlayerView>().FirstOrDefault();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void AddEntity(AbstractEntity entity)
    {
        _entities.Add(entity);
    }

    public void RemoveEntity(AbstractEntity entity)
    {
        _entities.Remove(entity);
    }

    private List<T> Get<T>() where T : AbstractEntity
    {
        return _entities.Where(x => x is T).OfType<T>().ToList();
    }
}