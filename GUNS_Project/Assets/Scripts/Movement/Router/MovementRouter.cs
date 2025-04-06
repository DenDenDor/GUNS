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
                view.MoveTo(model.Movement.GetPosition());
            }

            if (model is PlayerModel)
            {
                Debug.Log("POP " + model.Movement);
            }
        }
    }

    public void Exit()
    {
        
    }
}