using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractCurrenyRouter<T, U, W> : IRouter where T : AbstractCurrencyPickUp where U : AbstractCurrencyWindow  where W : AbstractCurrencyPressurePlateView
{
    private Coroutine _coroutine;

    private readonly Dictionary<AbstractPressurePlateView, int> _pressurePlatesByAmount = new();
    private readonly Dictionary<AbstractPressurePlateView, int> _startMaxPrice = new();

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

            int randomNumber = (int) Random.Range(4, 10);

            _pressurePlatesByAmount.Add(plate, randomNumber);
            _startMaxPrice.Add(plate, randomNumber);
            
            plate.UpdateBar(0);
            plate.UpdatePrice(randomNumber);
        }
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
        
        while (IsAbleToBuy && _pressurePlatesByAmount[view] > 0)
        {
            Buy();
            _pressurePlatesByAmount[view]--;

            view.UpdateBar(1 - (float) _pressurePlatesByAmount[view] / _startMaxPrice[view]);
            
            Plates.FirstOrDefault(x=>x == view)?.UpdatePrice(_pressurePlatesByAmount[view]);

            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public abstract void Exit();
}
