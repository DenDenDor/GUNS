using UnityEngine;

public class PlayerView : AbstractEntity
{
    public void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }
}
