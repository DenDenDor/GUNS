using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldRouter : AbstractCurrenyRouter<GoldPickUp, GoldWindow, GoldPressurePlateView>
{
    protected override string PathToPrefab => "Prefabs/Gold";
    protected override bool IsAbleToBuy => InventoryController.Instance.GoldCount > 0;
    
    protected override void Buy()
    {
        InventoryController.Instance.TakeGold();
    }
    
    public override void Init()
    {
        for (int i = 0; i < 20; i++)
        {
            CreateTo(UiController.Instance.GetWindow<SilverWindow>().StartPoint.position);
        }    
        
        Currency.CreatedGold += OnCreatedGold;
        Currency.InitGold += CreateTo;

        foreach (var silver in Currency.Golds)
        {
            OnCreatedGold(silver);
        }
        
        SubscribePlates();
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