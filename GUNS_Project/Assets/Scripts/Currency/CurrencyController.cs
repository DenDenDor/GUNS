using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class CurrencyController : MonoBehaviour
{
    private List<AbstractCurrencyPickUp> _pickUps = new();

    public event Action<SilverPickUp> CreatedSilver;
    
    public event Action<GoldPickUp> CreatedGold;

    public event Action<Vector3> InitSilver;

    public event Action<Vector3> InitGold;

    public List<SilverPickUp> Silvers => Get<SilverPickUp>();
    
    public List<GoldPickUp> Golds => Get<GoldPickUp>();
    
    private static CurrencyController _instance;

    public static CurrencyController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CurrencyController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("CurrencyController not found!");
                }
            }

            return _instance;
        }
    }


    private List<T> Get<T>() where T : AbstractCurrencyPickUp
    {
        return _pickUps.Where(x => x is T).OfType<T>().ToList();
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public void CreateSilver(Vector3 position)
    {
        InitSilver?.Invoke(position);
    }    
    
    public void CreateGold(Vector3 position)
    {
        InitGold?.Invoke(position);
    }

    public void AddPickUp(AbstractCurrencyPickUp pickUp)
    {
        _pickUps.Add(pickUp);

        if (pickUp is GoldPickUp gold)
            CreatedGold?.Invoke(gold);
        else if (pickUp is SilverPickUp silver) 
            CreatedSilver?.Invoke(silver);
    }
}