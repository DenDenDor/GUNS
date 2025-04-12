using System.Linq;
using UnityEngine;

public class BattleRouter : IRouter
{
    public void Init()
    {
        EntityController.Instance.Removed += OnRemoved;
    }

    private void OnRemoved()
    {
        if (EntityController.Instance.FullEntities.Keys.Count(x=>x is SoldierView) == 0)
        {
            BattleController.Instance.Restart();
        }
    }

    public void Exit()
    {
        EntityController.Instance.Removed -= OnRemoved;
    }
}