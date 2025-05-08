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
        
        UiController.Instance.GetWindow<BombWindow>().Created += Entered;
    }
   
    private Transform GetTarget()
    {
        return Plate.transform;
    }
    
    private void Entered(Transform a)
    {
        Close();

    }

    protected override void OnClose()
    {
        UiController.Instance.GetWindow<BombWindow>().Created -= Entered;
        
        ExitArrow();
    }
}
