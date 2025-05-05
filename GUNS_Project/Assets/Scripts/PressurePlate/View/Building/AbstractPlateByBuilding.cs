using System.Collections;
using UnityEngine;

public abstract class AbstractPlateByBuilding : MonoBehaviour
{
    [SerializeField] private BuildingType _buildingType;
    
    private void Awake()
    {
        switch (_buildingType)
        {
            case BuildingType.Plane or BuildingType.Bomb:
                break;
            case BuildingType.Barrack or BuildingType.Tank:
                StartCoroutine(BuildBarrack());
                break;
            default:
                StartCoroutine(BuildSpecial());
                break;
        }
    }

    private IEnumerator BuildBarrack()
    {
        yield return null;
        yield return null;
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.transform.localRotation = Quaternion.Euler(90f, -90f, 0);

        // rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // middle center
        // rectTransform.anchorMax = new Vector2(0.5f, 0.5f); // middle center
        rectTransform.transform.localPosition = new Vector3(4.17f, 0.073f, -0.02f);
    }

    private IEnumerator BuildSpecial()
    {
        yield return null;
        yield return null;
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.transform.localScale = Vector3.one;
        rectTransform.transform.localRotation = Quaternion.identity;

        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // middle center
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f); // middle center
        rectTransform.transform.localPosition = Vector3.zero;

        BoxCollider boxCollider = GetComponent<BoxCollider>();
            
        boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, 42.4f);

        rectTransform.anchoredPosition = Vector2.zero;
    }

}
