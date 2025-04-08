using UnityEngine;

public class MovementRouter : IRouter
{
    public void Init()
    {
        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
        foreach (var item in EntityController.Instance.FullEntities)
        {
            AbstractEntity view = item.Key;
            EntityModel model = item.Value;

            if (model.Movement != null)
            {
                Vector3 pos = model.Movement.GetPosition();

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