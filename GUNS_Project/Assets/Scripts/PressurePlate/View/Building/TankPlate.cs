using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TankPlate : AbstractPlateByBuilding, IPriceDisplayed, ICurrencyDisplay
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _currencyIcon;
    
    public void DisplayPrice(int amount)
    {
        _text.text = amount.ToString();
    }

    public void DisplaySprite(Sprite sprite)
    {
        _currencyIcon.sprite = sprite;
    }
}
