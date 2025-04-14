using System;
using UnityEngine;

public class FlagView : MonoBehaviour
{
    private bool _isTriggered;
    
    public event Action Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerView>() && _isTriggered == false)
        {
            Entered?.Invoke();
            _isTriggered = true;
        }
    }
}
