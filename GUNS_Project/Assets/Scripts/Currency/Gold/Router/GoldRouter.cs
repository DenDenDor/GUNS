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
            plate.UpdateBar(0);
            plate.Entered += OnEntered;
            plate.Exited += OnExited;
        }
    }

    private void OnExited()
    {
        CoroutineController.Instance.StopCoroutine(_coroutine);
    }

    private void OnEntered()
    {
        _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        float time = 0;

        while (time < 0.4f)
        {
            time += Time.deltaTime;
            
            yield return null;
        }

        Debug.LogError("GOLD COUNT " + InventoryController.Instance.GoldCount);

        while (InventoryController.Instance.GoldCount > 0)
        {
            InventoryController.Instance.TakeGold();
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