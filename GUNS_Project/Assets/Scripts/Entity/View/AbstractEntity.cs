using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour, IMoveTo
{
    public int Health;

    public abstract void MoveTo(Vector3 getPosition);
}
