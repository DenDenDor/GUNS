using System.Collections.Generic;
using UnityEngine;

public class AttackRouter : IRouter
{
    public void Init()
    {
        UpdateController.Instance.Add(OnUpdate);

        BulletController.Instance.Added += OnAdd;
    }

    private void OnAdd(BulletView view)
    {
        view.Triggered += OnTriggered;
    }

    private void OnTriggered(BulletView bullet, AbstractEntity entity)
    {
        BulletModel model = BulletController.Instance.Bullets[bullet];
        
        if (model.Entity.GetType() == entity.GetType())
        {
            model.Attack = new MiddleAttack(5, entity);
            model.Attack.Attack();
            Object.Destroy(bullet.gameObject);
        }
    }

    private void OnUpdate()
    {
        List<AbstractEntity> entities = AttackController.Instance.Entities;

        if (entities.Count == 0)
        {
            return;
        }
        
        foreach (var view in entities)
        {
            EntityModel model = EntityController.Instance.FullEntities[view];

            if (model.Attack != null && model.Attack.IsCooldown)
            {
                model.Attack.Attack();
                AttackController.Instance.Attack(view);
            }
        }
    }

    public void Exit()
    {
        BulletController.Instance.Added -= OnAdd;
    }
}