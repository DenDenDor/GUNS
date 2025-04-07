using UnityEngine;

public class GoldRouter : AbstractCurrenyRouter<GoldPickUp, GoldWindow>
{
    protected override string PathToPrefab => "Prefabs/Gold";

    public override void Init()
    {
        for (int i = 0; i < 7; i++)
        {
            Window.Create(Prefab, UiController.Instance.GetWindow<SilverWindow>().StartPoint);
        }    
        
        Currency.CreatedGold += OnCreatedGold;

        foreach (var silver in Currency.Golds)
        {
            OnCreatedGold(silver);
        }
    }
    
    private void OnCreatedGold(GoldPickUp obj)
    {
        obj.PickedUp += OnPickedUp;
    }

    public override void Exit()
    {
        
    }
    
}