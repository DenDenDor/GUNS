using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpEducationStep : AbstractEducationStep
{
    [SerializeField] private int _silverCount = 5;
    [SerializeField] private int _goldCount = 5;
    [SerializeField] private bool _isShowCurrency = true;

    private int _allCurrency;
    
    protected override void OnOpen()
    {
        InventoryController.Instance.UpdatedCount += OnUpdatedCount;
        
        UpdateAmount();

        EnterArrow(GetArrow);
    }

    private Transform GetArrow()
    {
        return CurrencyController.Instance.PickUps.Where(x => x != null)
            .Select(x => x.transform)
            .Except(InventoryController.Instance.PickUps.Where(x => x != null).Select(x => x.transform))
            .Select(x => x.transform)
            .OrderBy(obj =>
                Vector3.Distance(obj.transform.position, EntityController.Instance.Player.transform.position))
            .FirstOrDefault();
    }

    private void UpdateAmount()
    {
        int all = _goldCount + _silverCount;

        if (_allCurrency > all)
        {
            _allCurrency = all;
        }

        if (_isShowCurrency)
        {
            UiController.Instance.GetWindow<EducationWindow>().UpdateAmountText($"{_allCurrency} / {all}");
        }
    }

    private void OnUpdatedCount()
    {
        _allCurrency++;
        
        UpdateAmount();

        if (InventoryController.Instance.SilverCount >= _silverCount && InventoryController.Instance.GoldCount >= _goldCount)
        {
            Close();
        }
    }

    protected override void OnClose()
    {
         ExitArrow();
        
        InventoryController.Instance.UpdatedCount -= OnUpdatedCount;
    }

    protected override void OnUpdate()
    {
        
    }
}
