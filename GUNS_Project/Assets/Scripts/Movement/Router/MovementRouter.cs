using System.Collections.Generic;
using UnityEngine;

public class MovementRouter : IRouter
{
    public void Init()
    {
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        Dictionary<IMoveTo, IMovement> viewsByModels = new();

        foreach (var item in BulletController.Instance.Bullets)
        {
            viewsByModels.Add(item.Key, item.Value.Movement);
        }    
        
        foreach (var item in EntityController.Instance.FullEntities)
        {
            viewsByModels.Add(item.Key, item.Value.Movement);
        }
        
        CameraController.Instance.Deconstructor(out IMoveTo cameraView, out IMovement cameraModel);
        viewsByModels.Add(cameraView, cameraModel);
        
        foreach (var item in viewsByModels)
        {
            IMoveTo view = item.Key;
            IMovement model = item.Value;
            
            if (model != null)
            {
                Vector3 pos = model.GetPosition();

                if (pos != Vector3.zero)
                {
                    view.MoveTo(pos);
                }
            }
        }
    }

    public void Exit()
    {
        
    }
}