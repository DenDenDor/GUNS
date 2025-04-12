using TMPro;
using UnityEngine;

public abstract class AbstractCurrencyPressurePlateView : AbstractPressurePlateView
{
    [SerializeField] private TextMeshProUGUI _text;

    public void UpdatePrice(int amount)
    {
        _text.text = amount.ToString();
    }
}
