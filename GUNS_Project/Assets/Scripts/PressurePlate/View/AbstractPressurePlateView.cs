using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractPressurePlateView : MonoBehaviour
{
    [SerializeField] private Image _bar;

    private bool _isFilled;
    
    public event Action<AbstractPressurePlateView> Entered;
    public event Action<AbstractPressurePlateView> FilledIn;
    public event Action Exited;
    public event Action Reseted;
    
    public void UpdateBar(float fillness)
    {
        _bar.fillAmount = fillness;

        if (fillness == 1 && _isFilled == false)
        {
            FilledIn?.Invoke(this);
            _isFilled = true;
        }
    }

    public void FillIn()
    {
        FilledIn?.Invoke(this);
    }

    public void Reset()
    {
        _isFilled = false;
        Reseted?.Invoke();
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
