using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SilverRouter : AbstractCurrenyRouter<SilverPickUp, SilverWindow, SilverPressurePlateView>
{
    protected override bool IsAbleToBuy => Inventory.SilverCount > 0;
    protected override Sprite GetCurrencySprite => Currency.SilverSprite;

    private InventoryController Inventory => InventoryController.Instance;

    protected override void Buy()
    {
        Inventory.TakeSilver();
    }

    public override void Init()
    {
        WaveController.Instance.StartedNewWave += OnStartedNewWave;
        
        Currency.CreatedSilver += OnCreatedSilver;
        Currency.InitSilver += CreateTo;

        foreach (var silver in Currency.Silvers)
        {
            OnCreatedSilver(silver);
        }
        
        
        BuildingController.Instance.GeneratedPoints += OnGeneratedPoints; 
    }

    private void OnStartedNewWave()
    {
        for (int i = 0; i < 32; i++)
        {
            CreateTo(Inventory.ResourcePoint.position);
        }
        
        SubscribePlates();
    }

    private void CreateTo(Vector3 position)
    {
        Window.CreateCurrency(Prefab, position);
    }

    public override void Exit()
    {
    }
    
    
    private void OnCreatedSilver(SilverPickUp obj)
    {
        obj.PickedUp += OnPickedUp;
    }
}