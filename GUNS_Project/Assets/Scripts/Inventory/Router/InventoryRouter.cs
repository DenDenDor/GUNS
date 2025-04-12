using UnityEngine;

public class InventoryRouter : IRouter
{
    public void Init()
    {
        WaveController.Instance.Cleared += OnCleared;
    }

    private void OnCleared()
    {
        CurrencyController.Instance.ClearAll();
    }

    public void Exit()
    {
        WaveController.Instance.Cleared -= OnCleared;
    }
}