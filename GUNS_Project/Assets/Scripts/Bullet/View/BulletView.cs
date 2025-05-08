using System;
using UnityEngine;

public class BulletView : MonoBehaviour, IMoveTo
{
    [SerializeField] private ParticleSystem _small;
    [SerializeField] private ParticleSystem _medium;

    private bool _isTriggered;
    
    public event Action<BulletView, AbstractEntity> Triggered;

    private void Start()
    {
        Destroy(gameObject, 8);
    }

    public void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AbstractEntity>(out AbstractEntity entity) && _isTriggered == false)
        {
            Triggered?.Invoke(this, entity);
            _isTriggered = true;
        }
    }

    public void Enter(AbstractEntity entity, float damage)
    {
        if (HealthController.Instance.GetByEntity(entity).Health - damage > 0)
        {
            ParticleSystem particleSystem =  Instantiate(_small, transform.position, transform.rotation);
            particleSystem.Play();
            Destroy(particleSystem.gameObject, 2.5f);
        }
        else
        {
            ParticleSystem particleSystem = Instantiate(_medium, transform.position, transform.rotation);
            particleSystem.Play();
            Destroy(particleSystem.gameObject, 2.5f);
        }
    }
}
