using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    public int Health;

    public abstract void MoveTo(Vector3 getPosition);
}
