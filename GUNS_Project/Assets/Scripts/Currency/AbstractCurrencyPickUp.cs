using System;
using UnityEngine;

public abstract class AbstractCurrencyPickUp : MonoBehaviour
{
    public bool IsPickedUp { get; private set; }
    
    public event Action<AbstractCurrencyPickUp> PickedUp;
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsPickedUp == false && other.GetComponent<PlayerTriggerView>())
        {
            PickedUp?.Invoke(this);

            IsPickedUp = true;
        }
    }
}
