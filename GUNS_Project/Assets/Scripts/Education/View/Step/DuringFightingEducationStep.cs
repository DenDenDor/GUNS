using UnityEngine;

public class DuringFightingEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        BattleController.Instance.EndedBattle += OnEndBattle;
    }

    private void OnEndBattle()
    {
        Close();
    }

    protected override void OnClose()
    {
        BattleController.Instance.EndedBattle -= OnEndBattle;
    }
}
