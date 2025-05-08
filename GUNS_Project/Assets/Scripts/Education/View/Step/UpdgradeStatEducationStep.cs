using UnityEngine;

public class UpdgradeStatEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        UpgradeController.Instance.StoppedUpgraded += Entered;
    }
    
    private void Entered()
    {
        Close();
    }

    protected override void OnClose()
    {
        UpgradeController.Instance.StoppedUpgraded -= Entered;
    }
}
