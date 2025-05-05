using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelPlate : AbstractPlateByBuilding, IPriceDisplayed, IValueDisplay, ICurrencyDisplay
{
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private Image _currencyIcon;


    public void DisplayPrice(int amount)
    {
        _price.text = amount.ToString();
    }

    public void DisplayValue(int amount)
    {
        _value.text = amount.ToString();
    }

    public void DisplaySprite(Sprite sprite)
    {
        _currencyIcon.sprite = sprite;
    }
}
