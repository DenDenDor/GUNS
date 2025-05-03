using System.Collections;
using System.Linq;
using UnityEngine;

public class PickUpEducationStep : AbstractEducationStep
{
    private Coroutine _coroutine;
    
    protected override void OnOpen()
    {
        InventoryController.Instance.UpdatedCount += OnUpdatedCount;
        _coroutine = StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            Transform nearest = CurrencyController.Instance.PickUps.Where(x=>x != null).
                Select(x=>x.transform).Except(InventoryController.Instance.PickUps.Where(x=>x != null).Select(x=>x.transform)).
                Select(x => x.transform)
                .OrderBy(obj =>
                    Vector3.Distance(obj.transform.position, EntityController.Instance.Player.transform.position))
                .FirstOrDefault();

            Debug.Log("A" + EntityController.Instance.Player);
            LookAtObject.Appear(nearest);

        }
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
         StopCoroutine(_coroutine);
         
         LookAtObject.Disappear();

        InventoryController.Instance.UpdatedCount -= OnUpdatedCount;
    }

    protected override void OnUpdate()
    {
//var saveData = SDKMediator.Instance.GenerateSaveData();
        
        
    }
}
