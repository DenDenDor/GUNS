using UnityEngine;

public class UseBombEducationStep : AbstractEducationStep
{
    private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;

    private AbstractPressurePlateView Plate =>
        PressurePlateController.Instance.PressurePlateViewsByPoints[AllyPoint.BombPoint];

    private FlagView _flagView;
   
    protected override void OnOpen()
    {
        EnterArrow(GetTarget);

        Plate.FilledIn += Entered;
    }
   
    private Transform GetTarget()
    {
        return Plate.transform;
    }
    
    private void Entered(AbstractPressurePlateView a)
    {
        Close();

    }

    protected override void OnClose()
    {
        Plate.FilledIn -= Entered;
        
        ExitArrow();
    }
}
