using UnityEngine;

public class SilverRouter : IRouter
{
    private CurrencyController Currency => CurrencyController.Instance;

    private SilverWindow Window => UiController.Instance.GetWindow<SilverWindow>();
    
    public void Init()
    {
        SilverPickUp prefab = Resources.Load<SilverPickUp>("Prefabs/Silver");
        
        for (int i = 0; i < 6; i++)
        {
            Window.Create(prefab, Window.StartPoint);
        }
        
        Currency.CreatedSilver += OnCreatedSilver;

        foreach (var silver in Currency.Silvers)
        {
            OnCreatedSilver(silver);
        }
        
    }

    private void OnCreatedSilver(SilverPickUp obj)
    {
        obj.PickedUp += OnPickedUp;
    }

    private void OnPickedUp(AbstractCurrencyPickUp obj)
    {
        InventoryController.Instance.AddPickUp(obj);
        obj.PickedUp -= OnPickedUp;
    }

    public void Exit()
    {
        
    }
}