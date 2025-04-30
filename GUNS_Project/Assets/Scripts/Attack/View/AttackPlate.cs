using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlate : AbstractPlateByBuilding
{
    [SerializeField] private Image _fillness;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, 43);
    }

    public void UpdateBar(float fillness)
    {
        _fillness.fillAmount = fillness;
    }
}
