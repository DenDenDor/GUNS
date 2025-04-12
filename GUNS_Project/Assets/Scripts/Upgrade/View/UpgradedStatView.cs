using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradedStatView : MonoBehaviour
{
    [Header("Texts")]

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _progressAmount;

    [Header("Icons")] 
    [SerializeField] private Image _icon;
    [SerializeField] private Image _progressBar;

    [Header("Buttons")]
    [SerializeField] private Button _adButton;
    [SerializeField] private Button _payButton;

    public event Action<UpgradedStatView> WatchedAd;
    public event Action<UpgradedStatView> Bought;

    private void Awake()
    {
        _adButton.onClick.AddListener(() => WatchedAd?.Invoke(this));
        _payButton.onClick.AddListener(() => Bought?.Invoke(this));
    }

    public void UpdateSprite(Sprite getUpgradeSprite)
    {
        _icon.sprite = getUpgradeSprite;
    } 
    
    public void UpdateProgressAmount(int previousLevel, int nextLevel)
    {
        _progressAmount.text = $"{previousLevel} / {nextLevel}";
    }

    public void UpdateProgressBar(float fillAmount)
    {
        Debug.Log(fillAmount + " FILL AMOUNT ! ! !");
        _progressBar.fillAmount = fillAmount;
    }
}
