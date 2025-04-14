using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour, IMoveTo
{
    [SerializeField] protected Transform _child;
    [SerializeField] protected EntityArm _armWithGun;

    public Transform Child => _child;
    public EntityArm ArmWithGun => _armWithGun;

    public int Health;

    public abstract void MoveTo(Vector3 getPosition);
}
