using System;
using System.Collections;
using UnityEngine;

public class MiddleAttack : IAttack
{
    private float _waitTime = 5;
    private Func<float> _damage;
    private AbstractEntity _toAttack;

    public MiddleAttack(Func<float> damage, AbstractEntity toAttack)
    {
        _damage = damage;
        _toAttack = toAttack;
    }

    public bool IsCooldown { get; private set; } = true;

    public void Attack()
    {
        IsCooldown = false;

        HealthModel health = HealthController.Instance.GetByEntity(_toAttack);

        health.TakeDamage(_damage());
    }
}
