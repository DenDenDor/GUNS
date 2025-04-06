using UnityEngine;

public class PressurePlateRouter : IRouter
{
    public void Init()
    {
        FillingUpPressurePlateView prefab = Resources.Load<FillingUpPressurePlateView>("Prefabs/FillingUpPressurePlateView");

    }

    public void Exit()
    {
        
    }
}