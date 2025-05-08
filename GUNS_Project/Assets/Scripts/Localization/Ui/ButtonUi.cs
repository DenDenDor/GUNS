using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUi : MonoBehaviour
{
    private Button _button;
    private Vector3 _originalScale;
    private Coroutine _scaleCoroutine;

    public event Action Clicked;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Clicked?.Invoke();
        
        if (_scaleCoroutine != null)
        {
            StopCoroutine(_scaleCoroutine);
        }
        _scaleCoroutine = StartCoroutine(ScaleAnimation());
    }

    private System.Collections.IEnumerator ScaleAnimation()
    {
        float duration = 0.4f; // Общая длительность анимации
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            // Используем Sin для плавного изменения масштаба
            float progress = Mathf.Sin((elapsed / duration) * Mathf.PI);
            transform.localScale = _originalScale * (1f + 0.1f * progress);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Убедимся, что в конце масштаб вернулся к исходному
        transform.localScale = _originalScale;
    }
}