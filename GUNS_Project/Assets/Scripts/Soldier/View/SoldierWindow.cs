using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SoldierWindow : AbstractWindowUi
{
    [SerializeField] private List<Transform> _points;
    
    public override void Init()
    {
        
    }

    public SoldierView CreateSolider(SoldierView soldierView)
    {
        return Instantiate(soldierView, _points.GetRandomRepeatElement().position, quaternion.identity);
    }
}