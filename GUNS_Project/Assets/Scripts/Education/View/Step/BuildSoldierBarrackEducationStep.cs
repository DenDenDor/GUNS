using System.Collections;
using System.Linq;
using UnityEngine;

public class BuildSoldierBarrackEducationStep : AbstractEducationStep
{
    [SerializeField] private int _id;
    
    protected override void OnOpen()
    {
        BuildingController.Instance.CreatedBuilding += OnCreatedBuilding;

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        EnterArrow(GetTarget);
    }

    private Transform GetTarget()
    {
        return         FindObjectsOfType<BarrackPlate>().ToArray()[_id].transform;
    }

    private void OnCreatedBuilding(BuildingType obj)
    {
        if (obj == BuildingType.Barrack)
        {
            Close();
        }
    }

    protected override void OnClose()
    {
        BuildingController.Instance.CreatedBuilding -= OnCreatedBuilding;
        ExitArrow();
    }
}
