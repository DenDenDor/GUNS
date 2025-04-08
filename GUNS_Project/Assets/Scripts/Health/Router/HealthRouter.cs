using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthRouter : IRouter
{
    
    public void Init()
    {
        EntityController.Instance.Added += OnAdd;
    }

    private void OnAdd(AbstractEntity obj)
    {
        int value = Random.Range(0, 100);

        if (obj is SoldierView)
        {
            value = 50;
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
            Object.Destroy(entity.gameObject);
            EntityController.Instance.RemoveEntity(entity);
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
        HealthController.Instance.GetByHealth(healthModel).Health =  (int) healthModel.Health;
    }

    public void Exit()
    {
        EntityController.Instance.Added -= OnAdd;
    }
}