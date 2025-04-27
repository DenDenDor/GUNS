using UnityEngine;

public class EntityHead : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _particleSystem;
    
    private float _time;
    private bool _isAppeared;

    private void Update()
    {
        if (_isAppeared)
        {
            return;
        }
        
        _time += Time.deltaTime;

        if (_time > 0.05f && transform.position.y < 0.9f)
        {
            ParticleSystem particleSystem = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            Destroy(particleSystem.gameObject, 5);
            _isAppeared = true;
        }
    }
}
