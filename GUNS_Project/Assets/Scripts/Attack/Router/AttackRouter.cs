using System.Collections.Generic;
using UnityEngine;

public class AttackRouter : IRouter
{
    public void Init()
    {
        EntityController.Instance.Added += OnAdd;

        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnAdd(AbstractEntity obj)
    {
        
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
        EntityController.Instance.Added -= OnAdd;
    }
}