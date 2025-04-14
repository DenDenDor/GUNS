using UnityEngine;

public class InventoryRouter : IRouter
{
    private InventoryView _inventoryView;
    
    public void Init()
    {
        InventoryView prefab = FactoryController.Instance.FindPrefab<InventoryView>();

        Debug.Log(prefab + " FOUND SOMETGHIN!");
        
        _inventoryView = UiController.Instance.GetWindow<InventoryWindow>().CreateInventory(prefab);
        
        WaveController.Instance.Cleared += OnCleared;
        
        InventoryController.Instance.UpdatedCount += OnUpdateCount;
        WaveController.Instance.StartedNewWave += OnUpdateCount;
    }

    private void OnUpdateCount()
    {
        _inventoryView.UpdateGold(InventoryController.Instance.GoldCount);
        _inventoryView.UpdateSilver(InventoryController.Instance.SilverCount);
    }

    private void OnCleared()
    {
        CurrencyController.Instance.ClearAll();
    }

    public void Exit()
    {
        WaveController.Instance.Cleared -= OnCleared;
        
        InventoryController.Instance.UpdatedCount -= OnUpdateCount;
        WaveController.Instance.StartedNewWave -= OnUpdateCount;
    }
}