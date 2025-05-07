using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TankPlate : AbstractPlateByBuilding, IPriceDisplayed, ICurrencyDisplay
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _currencyIcon;
    
    private int _max;

    private Image _fill;

    private float _targetFill;

    private float _threshold = 0.001f;

    private void Start()
    {
        _fill = FindFill();
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

        _text.text = amount.ToString();
        
        if (amount == 0)
        {
            FindObject().gameObject.SetActive(false);
        }
    }

    public void DisplaySprite(Sprite sprite)
    {
        _currencyIcon.sprite = sprite;
    }
    
    private Image FindObject()
    {
        return GetComponentsInChildren<Image>().FirstOrDefault(x => x.name == "Outline");
    }
}
