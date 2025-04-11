using Unity.Mathematics;
using UnityEngine;

public class RotateRouter : IRouter
{
    public void Init()
    {
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        foreach (var item in EntityController.Instance.RotatableViews)
        {
            AbstractEntity entity = item as AbstractEntity;
            
            IRotation rotation = EntityController.Instance.FullEntities[entity].Rotation;

            if (rotation != null)
            {
                item.Rotate(rotation.RotateTo());
            }
        }
    }

    public void Exit()
    {
        
    }
}