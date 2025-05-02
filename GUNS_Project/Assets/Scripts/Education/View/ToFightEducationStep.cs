using UnityEngine;

public class ToFightEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        BattleController.Instance.StartedBattle += OnStartBattle;
    }

    private void OnStartBattle()
    {
        Close();
    }

    protected override void OnClose()
    {
        BattleController.Instance.StartedBattle -= OnStartBattle;
    }
}
