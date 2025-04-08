using System.Collections.Generic;
using UnityEngine;

public class BulletRouter : IRouter
{
    private BulletWindow Window => UiController.Instance.GetWindow<BulletWindow>();

    private BulletView _prefab;

    public void Init()
    {
        _prefab = Resources.Load<BulletView>("Prefabs/Bullet");
        
        BulletController.Instance.Created += OnCreated;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        // foreach (var bullet in Window.BulletViews)
        // {
        //     bullet.MoveTo(bullet);
        // }
    }

    private void OnCreated(AbstractEntity thisEntity, AbstractEntity toAttackEntity)
    {
        BulletView view = Window.Create(_prefab, thisEntity.transform);
        IMovement movement = new ToMoveTowardsMovement(6, view.transform, toAttackEntity.transform);

        BulletModel model = new BulletModel();

        model.Movement = movement;
        model.Entity = toAttackEntity;

        BulletController.Instance.Add(view, model);

    }

    public void Exit()
    {
        
    }
}