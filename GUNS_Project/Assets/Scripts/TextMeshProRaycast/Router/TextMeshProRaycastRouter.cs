using TMPro;
using UnityEngine;

public class TextMeshProRaycastRouter : IRouter
{
    public void Init()
    {
        FactoryController.Instance.CreatedUi += OnCreatedUi;
    }

    private void OnCreatedUi(GameObject obj)
    {
        foreach (var text in obj.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.raycastTarget = false;
        }
    }

    public void Exit()
    {
        FactoryController.Instance.CreatedUi -= OnCreatedUi;
    }
}