using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SilverRouter : AbstractCurrenyRouter<SilverPickUp, SilverWindow, SilverPressurePlateView>
{
    protected override string PathToPrefab => "Prefabs/Silver";

    protected override bool IsAbleToBuy => Inventory.SilverCount > 0;

    private InventoryController Inventory => InventoryController.Instance;

    protected override void Buy()
    {
        Inventory.TakeSilver();
    }

    public override void Init()
    {
        for (int i = 0; i < 6; i++)
        {
            CreateTo(Inventory.ResourcePoint.position);
        }
        
        Currency.CreatedSilver += OnCreatedSilver;
        Currency.InitSilver += CreateTo;

        foreach (var silver in Currency.Silvers)
        {
            OnCreatedSilver(silver);
        }
        
        SubscribePlates();
        
        BuildingController.Instance.GeneratedPoints += OnGeneratedPoints; 
    }

    private void CreateTo(Vector3 position)
    {
        Window.Create(Prefab, position);
    }

    public override void Exit()
    {
    }
    
    
    private void OnCreatedSilver(SilverPickUp obj)
    {
        obj.PickedUp += OnPickedUp;
    }
}