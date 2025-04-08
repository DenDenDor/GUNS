using System;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    public event Action<BulletView, AbstractEntity> Triggered;
    
    public void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AbstractEntity>(out AbstractEntity entity))
        {
            Triggered?.Invoke(this, entity);
        }
    }
}
