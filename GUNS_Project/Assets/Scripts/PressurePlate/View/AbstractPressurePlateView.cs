using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractPressurePlateView : MonoBehaviour
{
    [SerializeField] private Image _bar;

    public event Action<AbstractPressurePlateView> Entered;
    public event Action<AbstractPressurePlateView> FilledIn;
    public event Action Exited;
    
    public void UpdateBar(float fillness)
    {
        _bar.fillAmount = fillness;

        if (fillness == 1)
        {
            FilledIn?.Invoke(this);
        }
    }

    public void FillIn()
    {
        FilledIn?.Invoke(this);
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
