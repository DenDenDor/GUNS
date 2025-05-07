using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlate : AbstractPlateByBuilding
{
    [SerializeField] private Image _fillness;
    
    private Image _fill;

    private float _targetFill;

    private float _threshold = 0.001f;

    private void Update()
    {
        _fill.fillAmount = _targetFill;

//        _fill.fillAmount = Mathf.MoveTowards(_fill.fillAmount, _targetFill, Time.deltaTime * 2);

        // Если разница меньше порога — схлопываем в целевое значение
        if (Mathf.Abs(_fill.fillAmount - _targetFill) < _threshold)
        {
        }
    }

    private Image FindFill()
    {
        return GetComponentsInChildren<Image>().FirstOrDefault(x => x.name == "Fill");
    }

    private void UpdateTargetFill(float amount)
    {
        _targetFill  = amount;
    }
    
    private IEnumerator Start()
    {
        _fill = FindFill();

        // yield return null;
        // rectTransform.transform.localRotation = Quaternion.identity;
        
        yield return new WaitForSeconds(0.1f);
        
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, 43);
    }

    public void UpdateBar(float fillness)
    {
        UpdateTargetFill(fillness);

       // _fillness.fillAmount = fillness;
    }
}
