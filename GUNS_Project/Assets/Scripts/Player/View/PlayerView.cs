using UnityEngine;

public class PlayerView : AbstractEntity
{
    public override void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }
}
