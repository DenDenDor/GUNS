using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour, IMoveTo
{
    [SerializeField] protected Transform _child;

    public Transform Child => _child;

    public int Health;

    public abstract void MoveTo(Vector3 getPosition);
}
