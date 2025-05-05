using System.Collections;
using UnityEngine;

public abstract class AbstractPlateByBuilding : MonoBehaviour
{
    [SerializeField] private bool _isWorking;
    
    private void Awake()
    {
        if (_isWorking)
        {
            StartCoroutine(Wait());
        }
        else
        {
            StartCoroutine(Wait2());
        }
    }

    private IEnumerator Wait2()
    {
        yield return null;
        yield return null;
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.transform.localRotation = Quaternion.Euler(90f, -90f, 0);

        // rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // middle center
        // rectTransform.anchorMax = new Vector2(0.5f, 0.5f); // middle center
        rectTransform.transform.localPosition = new Vector3(4.17f, 0.073f, -0.02f);
    }

    private IEnumerator Wait()
    {
        yield return null;
        yield return null;
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.transform.localScale = Vector3.one;
        rectTransform.transform.localRotation = Quaternion.identity;

        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // middle center
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f); // middle center
        rectTransform.transform.localPosition = Vector3.zero;
    }

}
