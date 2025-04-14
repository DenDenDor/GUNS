using System.Collections.Generic;
using UnityEngine;

public class HealthWindow : AbstractFactoryWindow
{
    [SerializeField] private float _height = 2;
    
    private List<EntityHead> _heads = new();
    
    public override void Init()
    {
        
    }
    
    public EntityHead CreateEntityHead(EntityHead prefab, Transform entityTransform)
    {
        EntityHead head = CreatePrefab(prefab, entityTransform.position  + new Vector3(0, _height, 0));
        _heads.Add(head);
        return head;
    }

    public void ClearAll()
    {
        for (int i = 0; i < _heads.Count; i++)
        {
            Destroy(_heads[i].gameObject);
        }
        
        _heads.Clear();
    }
}