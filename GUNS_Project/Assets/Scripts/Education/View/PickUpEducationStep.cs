using UnityEngine;

public class PickUpEducationStep : AbstractEducationStep
{
    protected override void OnOpen()
    {
        InventoryController.Instance.UpdatedCount += OnUpdatedCount;
    }

    private void OnUpdatedCount()
    {
        if (InventoryController.Instance.SilverCount > 5 && InventoryController.Instance.GoldCount > 5)
        {
            Close();
        }
    }

    protected override void OnClose()
    {
        InventoryController.Instance.UpdatedCount -= OnUpdatedCount;
    }

    protected override void OnUpdate()
    {
//var saveData = SDKMediator.Instance.GenerateSaveData();
        
        
    }
}
