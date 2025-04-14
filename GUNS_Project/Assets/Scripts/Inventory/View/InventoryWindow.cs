using UnityEngine;

public class InventoryWindow : AbstractFactoryWindow
{
    [SerializeField] private Transform _point;
    public override void Init()
    {
        
    }

    public InventoryView CreateInventory(InventoryView prefab)
    {
       return CreatePrefab(prefab, _point, true);
    }
}