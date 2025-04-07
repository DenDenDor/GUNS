using UnityEngine;

public class SilverRouter : AbstractCurrenyRouter<SilverPickUp, SilverWindow>
{
    protected override string PathToPrefab => "Prefabs/Silver";

    public override void Init()
    {
        for (int i = 0; i < 6; i++)
        {
            Window.Create(Prefab, Window.StartPoint);
        }
        
        Currency.CreatedSilver += OnCreatedSilver;

        foreach (var silver in Currency.Silvers)
        {
            OnCreatedSilver(silver);
        }
    }

    public override void Exit()
    {
    }
    
    
    private void OnCreatedSilver(SilverPickUp obj)
    {
        obj.PickedUp += OnPickedUp;
    }
}