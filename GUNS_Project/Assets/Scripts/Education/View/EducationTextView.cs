using System;
using DG.Tweening;
using Localization;
using TMPro;
using UnityEngine;

public class EducationTextView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _amount;

    private Func<string> _getText;
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        UpdateAmountText("");
    }

    public void UpdateAmountText(string text)
    {
        _amount.text = text;
    }
    
    public void UpdateText(Func<string> getText)
    {
        _getText = getText;
    }

    private void Update()
    {
        if (_getText != null)
        {
            _text.text = _getText();
        }
    }

    public void Open()
    {
        transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBounce);
    }    
    
    public void Close()
    {
        transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBounce).OnComplete(() => UpdateAmountText(""));
    }
}
