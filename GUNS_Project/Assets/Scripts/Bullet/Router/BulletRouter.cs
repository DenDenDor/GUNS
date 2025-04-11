using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletRouter : IRouter
{
    private BulletWindow Window => UiController.Instance.GetWindow<BulletWindow>();
    private BulletController BulletController => BulletController.Instance;

    private BulletView _prefab;

    public void Init()
    {
        _prefab = Resources.Load<BulletView>("Prefabs/Bullet");
        
        BulletController.Created += OnCreated;

        WaveController.Instance.Updated += OnClear;
    }

    private void OnClear()
    {
        BulletController.ClearAll();
    }

    private void OnCreated(AbstractEntity thisEntity, AbstractEntity toAttackEntity, Func<float> damage, Func<float> speed)
    {
        if (thisEntity == null || toAttackEntity == null)
        {
            return;
        }
        
        BulletView view = Window.Create(_prefab, thisEntity.transform);
        IMovement movement = new ToMoveTowardsMovement(speed, view.transform, toAttackEntity.transform);

        BulletModel model = new BulletModel();

        model.Movement = movement;
        model.Entity = toAttackEntity;
        model.Damage = damage;

        BulletController.Add(view, model);
    }

    public void Exit()
    {
        
    }
}