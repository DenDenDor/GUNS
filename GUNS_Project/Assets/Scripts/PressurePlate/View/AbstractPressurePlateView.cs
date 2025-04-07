using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractPressurePlateView : MonoBehaviour
{
    [SerializeField] private Image _bar;

    public event Action<AbstractPressurePlateView> Entered;
    public event Action Exited;
    
    public void UpdateBar(float fillness)
    {
        _bar.fillAmount = fillness;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerView>())
        {
            Entered?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerTriggerView>())
        {
            Exited?.Invoke();
        }
    }
}
