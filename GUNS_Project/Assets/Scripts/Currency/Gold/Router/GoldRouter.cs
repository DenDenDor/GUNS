using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldRouter : AbstractCurrenyRouter<GoldPickUp, GoldWindow, GoldPressurePlateView>
{
    protected override string PathToPrefab => "Prefabs/Gold";
    protected override bool IsAbleToBuy => Inventory.GoldCount > 0;
    
    private InventoryController Inventory => InventoryController.Instance;

    protected override void Buy()
    {
        Inventory.TakeGold();
    }
    
    public override void Init()
    {
        for (int i = 0; i < 20; i++)
        {
            CreateTo(Inventory.ResourcePoint.position);
        }    
        
        Currency.CreatedGold += OnCreatedGold;
        Currency.InitGold += CreateTo;

        foreach (var silver in Currency.Golds)
        {
            OnCreatedGold(silver);
        }
        
        SubscribePlates();
        
        BuildingController.Instance.GeneratedPoints += OnGeneratedPoints; 
    }
    
    private void CreateTo(Vector3 position)
    {
        Window.Create(Prefab, position);
    }
    
  

    private void OnCreatedGold(GoldPickUp obj)
    {
        obj.PickedUp += OnPickedUp;
    }

    public override void Exit()
    {
        
    }
    
}