using System;
using UnityEngine;

public class ShootAttack : IAttack
{
    private float _waitTime = 5;
    private Func<float> _damage;
    private Func<float> _bulletSpeed;
    private AbstractEntity _toAttack;
    private AbstractEntity _thisEntity;

    public ShootAttack(Func<float> damage, Func<float> bulletSpeed, AbstractEntity thisEntity, AbstractEntity toAttack)
    {
        _damage = damage;
        _toAttack = toAttack;
        _thisEntity = thisEntity;
        _bulletSpeed = bulletSpeed;
    }

    public bool IsCooldown { get; private set; } = true;

    public void Attack()
    {
        IsCooldown = false;

        BulletController.Instance.Create(_thisEntity, _toAttack, _damage, _bulletSpeed);
    }
}
