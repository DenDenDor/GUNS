using System;
using TMPro;
using UnityEngine;

public abstract class AbstractCurrencyPressurePlateView : AbstractPressurePlateView
{
    [SerializeField] private TextMeshProUGUI _text;
    private IPriceDisplayed _priceDisplayed;
    
    private void Awake()
    {
        _priceDisplayed = GetComponent<IPriceDisplayed>();
    }

    public void UpdatePrice(int amount)
    {
        if (_text != null)
        {
            _text.text = amount.ToString();
        }

        if (_priceDisplayed != null)
        {
            _priceDisplayed.DisplayPrice(amount);
        }
    }
}
