using UnityEngine;

public class ToFightEducationStep : AbstractEducationStep
{
    private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;

    protected override void OnOpen()
    {
        BattleController.Instance.StartedBattle += OnStartBattle;
        
        EnterArrow(GetTarget);
    }

    private Transform GetTarget()
    {
        return AllyPoint.AttackButton.transform;
    }
    
    private void OnStartBattle()
    {
        Close();
    }

    protected override void OnClose()
    {
        BattleController.Instance.StartedBattle -= OnStartBattle;
        ExitArrow();
    }
}
