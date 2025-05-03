using System;
using System.Collections;
using Localization;
using TMPro;
using UnityEngine;

public class ProgressBarView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private DisplayLocalizedString _displayLocalizedString;
    [SerializeField] private float _maxLeft = 230.44f;
    [SerializeField] private float _animationSpeed = 1f;

    private Coroutine _currentCoroutine;
    private Func<string> _levelGet;

    public void UpdateBar(float targetX)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(AnimateBar(targetX));
    }

    public void UpdateLevel(int level)
    {
        _levelGet = () => $"{_displayLocalizedString.LocalizedText} {level}";
    }

    private void Update()
    {
        if (_levelGet != null)
        {
            _text.text = _levelGet();
        }
    }

    private IEnumerator AnimateBar(float targetX)
    {
        targetX = Mathf.Clamp01(targetX);
        float startLeft = _rectTransform.offsetMin.x;
        float targetLeft = Mathf.Lerp(0f, _maxLeft, targetX);
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * _animationSpeed;
            float currentLeft = Mathf.Lerp(startLeft, targetLeft, progress);
            _rectTransform.offsetMin = new Vector2(currentLeft, _rectTransform.offsetMin.y);
            yield return null;
        }

        _rectTransform.offsetMin = new Vector2(targetLeft, _rectTransform.offsetMin.y);
    }
}