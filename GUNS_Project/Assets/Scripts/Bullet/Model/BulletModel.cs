using System;
using UnityEngine;

public class BulletModel
{
    public IMovement Movement { get; set; }
    public IAttack Attack { get; set; }
    public AbstractEntity Entity { get; set; }
    public Func<float> Damage { get; set; }
}
