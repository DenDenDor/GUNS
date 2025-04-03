using UnityEngine;

public interface IAttack
{
    void Attack();
    bool IsCooldown { get; }
}
