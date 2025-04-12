using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryController : MonoBehaviour
{
    private List<AbstractCurrencyPickUp> _pickUps = new();
    
    private static InventoryController _instance;

    public static InventoryController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("InventoryController not found!");
                }
            }

            return _instance;
        }
    }

    public int GoldCount => Count<GoldPickUp>();
    public int SilverCount => Count<SilverPickUp>();

    public Transform ResourcePoint => WaveController.Instance.GenerateWaveInfo().ResourcePoint;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void AddPickUp(AbstractCurrencyPickUp currencyPickUp)
    {
        _pickUps.Add(currencyPickUp);
        
        currencyPickUp.transform.SetParent(EntityController.Instance.Player.CurrencyPoint);

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        _pickUps = _pickUps.OrderBy(x => x is GoldPickUp).ToList();

        float height = 0;
        
        foreach (var item in _pickUps)
        {
            item.transform.localPosition = new Vector3(0, height, 0);

            height += 0.3f;
        }
    }

    public void TakeGold()
    {
        TakeCurrency<GoldPickUp>();
    }  
    
    public void TakeSilver()
    {
        TakeCurrency<SilverPickUp>();
    }

    private void TakeCurrency<T>() where T : AbstractCurrencyPickUp
    {
        AbstractCurrencyPickUp pickUp = _pickUps.FirstOrDefault(x => x is T);

        if (pickUp != null)
        {
            _pickUps.Remove(pickUp);
            UpdatePosition();
            
            Destroy(pickUp.gameObject);
        }
    }

    private int Count<T>() where T : AbstractCurrencyPickUp
    {
       return _pickUps.Count(x=>x is T);
    }
}