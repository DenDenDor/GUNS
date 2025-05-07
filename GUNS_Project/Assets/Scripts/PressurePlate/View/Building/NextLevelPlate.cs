using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelPlate : AbstractPlateByBuilding, IPriceDisplayed, IValueDisplay, ICurrencyDisplay
{
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private Image _currencyIcon;

    private int _max;

    private Image _fill;

    private float _targetFill;

    private float _threshold = 0.001f;

    private void Start()
    {
        _fill = FindFill();

        _fill.fillAmount = 0;
    }

    private void Update()
    {
        _fill.fillAmount = Mathf.Lerp(_fill.fillAmount, _targetFill, Time.deltaTime * 2);

        // Если разница меньше порога — схлопываем в целевое значение
        if (Mathf.Abs(_fill.fillAmount - _targetFill) < _threshold)
        {
            _fill.fillAmount = _targetFill;
        }
    }

    private Image FindFill()
    {
        return GetComponentsInChildren<Image>().FirstOrDefault(x => x.name == "Fill");
    }

    private void UpdateTargetFill(int amount)
    {
        if (amount > _max)
        {
            _max = amount;
        }

        _targetFill  = 1 - (float) amount / _max;
    }
    
    public void DisplayPrice(int amount)
    {
        UpdateTargetFill(amount);

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
