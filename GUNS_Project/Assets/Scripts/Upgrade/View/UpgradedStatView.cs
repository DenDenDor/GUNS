using System;
using System.Collections;
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
    [SerializeField] private RectTransform _progressBar; // Изменили тип на RectTransform
    [SerializeField] private GameObject _maxLevelContainer;
    [SerializeField] private GameObject _progressContainer;

    [Header("Buttons")]
    [SerializeField] private Button _adButton;
    [SerializeField] private Button _payButton;

    [Header("Animation")]
    [SerializeField] private float _animationSpeed = 5f;
    
    private float _maxWidth;
    private Coroutine _animationCoroutine;

    public event Action<UpgradedStatView> WatchedAd;
    public event Action<UpgradedStatView> Bought;

    private void Awake()
    {
        _adButton.onClick.AddListener(() => WatchedAd?.Invoke(this));
        _payButton.onClick.AddListener(() => Bought?.Invoke(this));
        
        // Сохраняем максимальную ширину прогресс-бара
        _maxWidth = _progressBar.sizeDelta.x;
    }

    public void UpdateSprite(Sprite getUpgradeSprite)
    {
        _icon.sprite = getUpgradeSprite;
    } 
    
    public void UpdateProgressAmount(int previousLevel, int nextLevel)
    {
        _progressAmount.text = $"{previousLevel} / {nextLevel}";
    }


    private float rightAtZero = 72.05f;
    private float rightAtOne = -59;

    public void UpdateProgressBar(float fillAmount)
    {
        fillAmount = Mathf.Clamp01(fillAmount);
        
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        Debug.LogError(fillAmount + " FILL AMOUNT! ");

        _fillAmount =  1 -fillAmount;


        //_animationCoroutine = StartCoroutine(AnimateProgressBar(fillAmount));
    }

    private float _fillAmount;
    
    private void Update()
    {
        float newRight = Mathf.Lerp(rightAtZero, rightAtOne, _fillAmount);

        Vector2 offsetMax = _progressBar.offsetMax;
        offsetMax.x = newRight;
        _progressBar.offsetMax = offsetMax;
    }

    private IEnumerator AnimateProgressBar(float targetFill)
    {
        float startWidth = _progressBar.sizeDelta.x;
        float targetWidth = _maxWidth * targetFill;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * _animationSpeed;
            float currentWidth = Mathf.Lerp(startWidth, targetWidth, progress);
            _progressBar.sizeDelta = new Vector2(currentWidth, _progressBar.sizeDelta.y);
            yield return null;
        }

        _progressBar.sizeDelta = new Vector2(targetWidth, _progressBar.sizeDelta.y);
    }

    public void UpdateLevel(int level)
    {
        _level.text = $"{level} \n lvl";
    }

    public void ShowMaxLevelPanel()
    {
        _maxLevelContainer.SetActive(true);
        _progressContainer.SetActive(false);
    }
}