using UnityEngine;

public class PressurePlateRouter : IRouter
{
    public void Init()
    {
        PressurePlateController.Instance.Created += OnCreated;
    }

    private void OnCreated(Transform transform)
    {
        FillingUpPressurePlateView prefab = Resources.Load<FillingUpPressurePlateView>("Prefabs/FillingUpPressurePlateView");

        FillingUpPressurePlateView plateView = Object.Instantiate(prefab, transform);
        
        PressurePlateController.Instance.Register(transform, plateView);
    }

    public void Exit()
    {
        PressurePlateController.Instance.Created -= OnCreated;
    }
}