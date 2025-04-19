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

        WaveController.Instance.Cleared += OnClear;
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

        BulletView view = null;
        IMovement movement = null;

        if (thisEntity is PlayerView playerView)
        {
            view = Window.Create(_prefab, thisEntity.ArmWithGun.ShotPoint);

            Vector3 direction = playerView.Direction;
           // direction.y = 0;
            
            //movement = new ForwardMovement(speed, view.transform, direction );
            
            movement = new ToMoveTowardsMovement(speed, view.transform, toAttackEntity.transform);

            playerView.UpdateParticle();
            view.name = "PLAYER BULLET!";
        }
        else
        {
            view = Window.Create(_prefab, thisEntity.transform);
            movement = new ToMoveTowardsMovement(speed, view.transform, toAttackEntity.transform);
        }

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