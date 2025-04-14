using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthRouter : IRouter
{
    private EntityHead _prefab;

    private HealthWindow Window => UiController.Instance.GetWindow<HealthWindow>();
    
    public void Init()
    {
        _prefab = FactoryController.Instance.FindPrefab<EntityHead>();
        
        EntityController.Instance.Added += OnAdd;
        
        WaveController.Instance.Cleared += OnClear;
    }

    private void OnClear()
    {
        EntityController.Instance.ClearAll();
        Window.ClearAll();
    }

    private void OnAdd(AbstractEntity obj)
    {
        int value = Random.Range(0, 100);

        if (obj is SoldierView)
        {
            value = 50;
        }
        else if(obj is PlayerView)
        {
            value = 100;
        }
        
        HealthModel health = new HealthModel(value);
       
        health.Death += OnDeathTaken;
        health.TakenDamage += OnTakenDamage;
        
        HealthController.Instance.Add(obj, health);
        
        HealthController.Instance.GetByHealth(health).Health =  (int) health.Health;
        
    }

    private void OnDeathTaken(HealthModel health)
    {
        AbstractEntity entity = HealthController.Instance.GetByHealth(health);
        
        if (entity is not PlayerView)
        {
            EntityHead entityHead = Window.CreateEntityHead(_prefab, entity.transform);
            
            Object.Destroy(entity.gameObject);
            EntityController.Instance.RemoveEntity(entity);
        }
        else
        {
            HealthController.Instance.DeathPlayer();
        }

        Vector3 position = entity.transform.position;

        switch (entity)
        {
            case EnemyView:
                CurrencyController.Instance.CreateGold(position);
                break;
            case SoldierView:
                CurrencyController.Instance.CreateSilver(position);
                break;
        }
        
        health.Death -= OnDeathTaken;
    }

    private void OnTakenDamage(HealthModel healthModel)
    {
        if (HealthController.Instance.GetByHealth(healthModel) is PlayerView && healthModel.Health < 25)
        {
            HealthController.Instance.ShowLowPlayerHealth();
        }
        
        HealthController.Instance.GetByHealth(healthModel).Health =  (int) healthModel.Health;
    }

    public void Exit()
    {
        EntityController.Instance.Added -= OnAdd;
    }
}