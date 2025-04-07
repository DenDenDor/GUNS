using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PressurePlateRouter : IRouter
{
    public void Init()
    {
        PressurePlateController.Instance.Created += OnCreated;
    }

    private void OnCreated(Transform transform, PressurePlateType type)
    {
        AbstractPressurePlateView prefab = null;

        switch (type)
        {
            case PressurePlateType.FillingUp:
                prefab = Resources.Load<FillingUpPressurePlateView>("Prefabs/FillingUpPressurePlateView");
                break;
            case PressurePlateType.Gold:
                prefab = Resources.Load<GoldPressurePlateView>("Prefabs/GoldPressurePlateView");
                break;
            case PressurePlateType.Silver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        AbstractPressurePlateView plateView = Object.Instantiate(prefab, transform);
        
        PressurePlateController.Instance.Register(transform, plateView);
    }

    public void Exit()
    {
        PressurePlateController.Instance.Created -= OnCreated;
    }
}