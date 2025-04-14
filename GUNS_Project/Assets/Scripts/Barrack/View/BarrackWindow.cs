using System.Collections.Generic;
using UnityEngine;


public class BarrackWindow : AbstractFactoryWindow
{
    
    public override void Init()
    {
        
    }

    public AbstractBarrackView CreateBarrack(AbstractBarrackView barrack, Vector3 transformPosition)
    {
      return  CreatePrefab(barrack, transformPosition);
    }
}