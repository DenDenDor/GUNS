using UnityEngine;

public class SoldierRouter : IRouter
{
    public void Init()
    {
        SoldierView prefab = Resources.Load<SoldierView>("Prefabs/Soldier");

        SoldierView view = UiController.Instance.GetWindow<SoldierWindow>().CreateSolider(prefab);

    }

    public void Exit()
    {
        
    }
}