using UnityEngine;

public abstract class AbstractCurrenyRouter<T, U> : IRouter where T : AbstractCurrencyPickUp where U : AbstractCurrencyWindow
{
    protected abstract string PathToPrefab { get; }
    
    protected CurrencyController Currency => CurrencyController.Instance;

    protected U Window => UiController.Instance.GetWindow<U>();

    protected T Prefab => Resources.Load<T>(PathToPrefab);

    public abstract void Init();
    
    protected void OnPickedUp(AbstractCurrencyPickUp obj)
    {
        InventoryController.Instance.AddPickUp(obj);
        obj.PickedUp -= OnPickedUp;
    }

    public abstract void Exit();
}
