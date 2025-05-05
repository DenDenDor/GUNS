using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldRouter : AbstractCurrenyRouter<GoldPickUp, GoldWindow, GoldPressurePlateView>
{
    protected override bool IsAbleToBuy => Inventory.GoldCount > 0;
    
    private InventoryController Inventory => InventoryController.Instance;

    protected override void Buy()
    {
        Inventory.TakeGold();
    }
    
    protected override Sprite GetCurrencySprite => Currency.GoldSprite;

    public override void Init()
    {
        WaveController.Instance.StartedNewWave += OnStartedNewWave;
        
        Currency.CreatedGold += OnCreatedGold;
        Currency.InitGold += CreateTo;

        foreach (var silver in Currency.Golds)
        {
            OnCreatedGold(silver);
        }
        
        
        BuildingController.Instance.GeneratedPoints += OnGeneratedPoints; 
    }

    private void OnStartedNewWave()
    {
        StartSetPosition();

        for (int i = 0; i < 5; i++)
        {
            CreateTo(Inventory.ResourcePoint.Gold.position);
        }
        
        StopSetPosition();
        
        SubscribePlates();
    }

    private void CreateTo(Vector3 position)
    {
        Window.CreateCurrency(Prefab, position, _getPoint);
    }
    
  

    private void OnCreatedGold(GoldPickUp obj)
    {
        obj.PickedUp += OnPickedUp;
    }

    public override void Exit()
    {
        
    }
    
}