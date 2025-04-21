using System;
using UnityEngine;

public class PlaneBombView : MonoBehaviour
{
  [SerializeField] private Rigidbody _rigidbody;
  
  public event Action<PlaneBombView> Entered;

  private float _time;
  
  private void Update()
  {
    _time += Time.deltaTime;
    
    if (_time > 0.5f && _rigidbody.linearVelocity.y == 0)
    {
      Entered?.Invoke(this);
      
      Destroy(gameObject);
    }
  }
}
