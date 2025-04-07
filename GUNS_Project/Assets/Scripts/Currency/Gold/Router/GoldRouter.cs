using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldRouter : AbstractCurrenyRouter<GoldPickUp, GoldWindow>
{
    protected override string PathToPrefab => "Prefabs/Gold";

    private List<GoldPressurePlateView> Plates
    {
        get
        {
            List<GoldPressurePlateView> goldPressurePlateViews = PressurePlateController.Instance
                .PressurePlateViewsByPoints.Where(x => x.Value is GoldPressurePlateView).Select(x=>x.Value)
                .OfType<GoldPressurePlateView>().ToList();
            
            return goldPressurePlateViews;
        }
    }

    private Coroutine _coroutine;

    private Dictionary<AbstractPressurePlateView, int> _pressurePlatesByAmount = new();
    private Dictionary<AbstractPressurePlateView, int> _startMaxPrice = new();

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
        
        PressurePlateController.Instance.AddPressurePlate(UiController.Instance.GetWindow<SilverWindow>().StartPoint, PressurePlateType.Gold);

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

        while (time < 0.4f)
        {
            time += Time.deltaTime;
            
            yield return null;
        }

        Debug.LogError("GOLD COUNT " + InventoryController.Instance.GoldCount);

        while (InventoryController.Instance.GoldCount > 0 && _pressurePlatesByAmount[view] > 0)
        {
            InventoryController.Instance.TakeGold();
            _pressurePlatesByAmount[view]--;

            view.UpdateBar(1 - (float) _pressurePlatesByAmount[view] / _startMaxPrice[view]);
            
            Plates.FirstOrDefault(x=>x == view).UpdatePrice(_pressurePlatesByAmount[view]);

            yield return new WaitForSeconds(0.2f);
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