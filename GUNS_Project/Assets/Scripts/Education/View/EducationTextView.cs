using System;
using System.Collections;
using Localization;
using TMPro;
using UnityEngine;

public class EducationTextView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private float _animationDuration = 0.4f;
    [SerializeField] private float _bounceIntensity = 0.3f; // Сила "отскока"

    private Func<string> _getText;
    private Coroutine _scaleCoroutine;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        UpdateAmountText("");
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.4f);
        LocalizedInstaller.Instance.UpdatedLanguage += OnUpdateLanguage;
        OnUpdateLanguage();
    }

    private void OnUpdateLanguage()
    {
        if (_getText != null)
        {
            _text.text = _getText();
        }
    }

    public void UpdateAmountText(string text)
    {
        _amount.text = text;
    }
    
    public void UpdateText(Func<string> getText)
    {
        _getText = getText;
        OnUpdateLanguage();
    }

    public void Open()
    {
        if (_scaleCoroutine != null)
        {
            StopCoroutine(_scaleCoroutine);
        }
        _scaleCoroutine = StartCoroutine(ScaleAnimation(Vector3.one, true));
    }    
    
    public void Close()
    {
        if (_scaleCoroutine != null)
        {
            StopCoroutine(_scaleCoroutine);
        }
        _scaleCoroutine = StartCoroutine(ScaleAnimation(Vector3.zero, false, () => UpdateAmountText("")));
    }

    private IEnumerator ScaleAnimation(Vector3 targetScale, bool withBounce, Action onComplete = null)
    {
        Vector3 initialScale = transform.localScale;
        float time = 0f;

        while (time < _animationDuration)
        {
            time += Time.deltaTime;
            float progress = Mathf.Clamp01(time / _animationDuration);
            
            if (withBounce)
            {
                // Синусоидальный "отскок" в конце анимации
                float bounceProgress = Mathf.Clamp01((progress - 0.8f) / 0.2f) * Mathf.PI;
                float bounceFactor = Mathf.Sin(bounceProgress * 2f) * _bounceIntensity * (1f - progress);
                progress = Mathf.Min(progress, 0.8f) + bounceFactor;
            }
            
            transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            yield return null;
        }

        transform.localScale = targetScale;
        onComplete?.Invoke();
    }

    private void OnDestroy()
    {
        if (LocalizedInstaller.Instance != null)
        {
            LocalizedInstaller.Instance.UpdatedLanguage -= OnUpdateLanguage;
        }
    }
}