using UnityEngine;

public class ShootAttack : IAttack
{
    private float _waitTime = 5;
    private float _damage;
    private AbstractEntity _toAttack;
    private AbstractEntity _thisEntity;

    public ShootAttack(float damage, AbstractEntity thisEntity, AbstractEntity toAttack)
    {
        _damage = damage;
        _toAttack = toAttack;
        _thisEntity = thisEntity;
    }

    public bool IsCooldown { get; private set; } = true;

    public void Attack()
    {
        IsCooldown = false;

        BulletController.Instance.Create(_thisEntity, _toAttack);
    }
}
