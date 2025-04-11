using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EntityController : MonoBehaviour
{
    private Dictionary<AbstractEntity, EntityModel> _entities = new();

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


    public List<AbstractEntity> Entities => _entities.Keys.ToList();
    public Dictionary<AbstractEntity, EntityModel> FullEntities => _entities;

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
    public List<IRotatableView> RotatableViews => _entities.Keys.Where(x=>x is IRotatableView).OfType<IRotatableView>().ToList();

    public List<SoldierView> Soldiers => Get<SoldierView>();

    public PlayerView Player => Get<PlayerView>().FirstOrDefault();

    public event Action<AbstractEntity> Added;
    public event Action Removed;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void AddEntity(AbstractEntity entity, EntityModel model)
    {
        _entities.Add(entity, model);
        Added?.Invoke(entity);
    }

    public void RemoveEntity(AbstractEntity entity)
    {
        _entities.Remove(entity);
        Removed?.Invoke();
    }

    private List<T> Get<T>() where T : AbstractEntity
    {
        return _entities.Where(x => x.Key != null && x.Key is T).Select(x=>x.Key).OfType<T>().ToList();
    }

    public void ClearAll()
    {
        // for (int i = 0; i < Entities.Count; i++)
        // {
        //     AbstractEntity entity = Entities[i];
        //     
        //     if (entity is not PlayerView)
        //     {
        //         Destroy(entity.gameObject);
        //     }   
        // }
        //
        // _entities = _entities.Where(x => x.Key != null);
        
        _entities.DestroyAllMonoBehaviours();
        
        _entities.Clear();
    }
}