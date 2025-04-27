using System;
using UnityEngine;

public abstract class AbstractCurrencyPickUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem _coinExplosionCopper;
    
    public bool IsPickedUp { get; private set; }
    
    public event Action<AbstractCurrencyPickUp> PickedUp;
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsPickedUp == false && other.GetComponent<PlayerTriggerView>())
        {
            ParticleSystem particleSystem = Instantiate(_coinExplosionCopper, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

            particleSystem.startColor = CurrencyColor;
            
            Destroy(particleSystem.gameObject, 4);
            
            PickedUp?.Invoke(this);

            IsPickedUp = true;
        }
    }

    protected abstract Color CurrencyColor { get; set; }
}
