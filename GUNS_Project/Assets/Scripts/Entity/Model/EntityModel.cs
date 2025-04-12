using UnityEngine;

public abstract class EntityModel
{
    public Transform StartPoint;
    public IMovement Movement;
    public IAttack Attack;
    public IRotation Rotation;
}
