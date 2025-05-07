using System;
using UnityEngine;

public class JetPlate : AbstractPlateByBuilding
{
    private void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        boxCollider.center = new Vector3(0, -112.82f, 0);
        boxCollider.size = new Vector3(67, 95f, 70.61f);
    }
}
