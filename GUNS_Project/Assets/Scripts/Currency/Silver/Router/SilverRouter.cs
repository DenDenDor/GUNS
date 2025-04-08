using UnityEngine;

public class SilverRouter : AbstractCurrenyRouter<SilverPickUp, SilverWindow>
{
    protected override string PathToPrefab => "Prefabs/Silver";

    public override void Init()
    {
        for (int i = 0; i < 6; i++)
        {
            CreateTo(Window.StartPoint.position);
        }
        
        Currency.CreatedSilver += OnCreatedSilver;
        Currency.InitSilver += CreateTo;

        foreach (var silver in Currency.Silvers)
        {
            OnCreatedSilver(silver);
        }
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