using UnityEngine;

public abstract class EntityWindow : AbstractWindowUi
{
    public override void Init()
    {
        
    }

    protected AbstractEntity Add(AbstractEntity entity, EntityModel model)
    {
        EntityController.Instance.AddEntity(entity, model);

        return entity;
    }
}