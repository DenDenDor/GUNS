using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SoldierWindow : EntityWindow
{

    public override void Init()
    {
    }


    public SoldierView CreateSolider(SoldierView soldierView, Transform point, SoldierModel model)
    {
        SoldierView created = CreatePrefab(soldierView, point.position);

        Add(created, model);
        
        return created;
    } 
}