using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }
}
