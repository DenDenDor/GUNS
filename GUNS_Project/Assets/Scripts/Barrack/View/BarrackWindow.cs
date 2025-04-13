using System.Collections.Generic;
using UnityEngine;


public class BarrackWindow : AbstractFactoryWindow
{
    
    public override void Init()
    {
        
    }

    public BarrackView CreateBarrack(BarrackView barrack, Vector3 transformPosition)
    {
      return  CreatePrefab(barrack, transformPosition);
    }
}