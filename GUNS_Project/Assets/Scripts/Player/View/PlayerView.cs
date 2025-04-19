using System.Collections;
using UnityEngine;

public class PlayerView : AbstractEntity, IRotatableView
{
    [SerializeField] private Transform _currencyPoint;
    [SerializeField] private Transform _lookAtTransform;
    [SerializeField] private GameObject _particle;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private Transform _firstFirePoint;
    [SerializeField] private Transform _secondFirePoint;

    public Transform LookAtTransform => _lookAtTransform;

    public Transform CurrencyPoint => _currencyPoint;

    public Vector3 Direction => _firstFirePoint.position - _secondFirePoint.position;
    
    public override void MoveTo(Vector3 getPosition)
    {
        transform.position = getPosition;
    }

    public void Rotate(Quaternion toRotate)
    {
        _child.transform.rotation = toRotate;
    }

    public void UpdateTriggerView()
    {
        Collider myCollider = GetComponentInChildren<PlayerTriggerView>().GetComponent<Collider>();
        
        StartCoroutine(Wait(myCollider));
    }

    private IEnumerator Wait(Collider myCollider)
    {
        myCollider.enabled = false;
        yield return new WaitForEndOfFrame();
        myCollider.enabled = true;
    }

    public void UpdateParticle()
    {
        GameObject go = Instantiate(_particle, _gunPoint.position, Quaternion.identity);
        
        go.GetComponentInChildren<ParticleSystem>().Emit(1);
    }
}
