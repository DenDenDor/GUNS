using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractCurrenyRouter<T, U, W> : IRouter where T : AbstractCurrencyPickUp where U : AbstractCurrencyWindow  where W : AbstractCurrencyPressurePlateView
{
    private Coroutine _coroutine;


    private List<W> Plates
    {
        get
        {
            List<W> plateViews = PressurePlateController.Instance
                .PressurePlateViewsByPoints.Where(x => x.Value is W).Select(x=>x.Value)
                .OfType<W>().ToList();
            
            return plateViews;
        }
    }

    protected abstract string PathToPrefab { get; }
    protected abstract bool IsAbleToBuy { get; }

    protected abstract void Buy();
    
    protected CurrencyController Currency => CurrencyController.Instance;

    protected U Window => UiController.Instance.GetWindow<U>();

    protected T Prefab => Resources.Load<T>(PathToPrefab);

    public abstract void Init();
    
    protected void OnPickedUp(AbstractCurrencyPickUp obj)
    {
        InventoryController.Instance.AddPickUp(obj);
        obj.PickedUp -= OnPickedUp;
    }

    protected void SubscribePlates()
    {
        foreach (var plate in Plates)
        {
            plate.Entered += OnEntered;
            plate.Exited += OnExited;
            plate.Reseted += () => OnReset(plate);
            
            OnReset(plate);
        }
    }

    private void OnReset(W plate)
    {
        PressurePlateController.Instance.GetPriceBy(plate, out int current, out int max);
            
        plate.UpdatePrice(current);
        plate.UpdateBar(0);
    }

    private void OnExited()
    {
        CoroutineController.Instance.StopCoroutine(_coroutine);
    }

    private void OnEntered(AbstractPressurePlateView view)
    {
        _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown(view));
    }

    private IEnumerator Cooldown(AbstractPressurePlateView view)
    {
        float time = 0;

        float maxTime = 0.4f;
        
        while (time < maxTime)
        {
            time += Time.deltaTime;
            
            yield return null;
        }
        
        PressurePlateController.Instance.GetPriceBy(view, out int current, out int max);
        
        while (IsAbleToBuy && current > 0)
        {
            Buy();
            current--;

            PressurePlateController.Instance.UpdateCurrentPrice(view, current);

            view.UpdateBar(1 - (float) current / max);
            
            Plates.FirstOrDefault(x=>x == view)?.UpdatePrice(current);

            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public abstract void Exit();
}
