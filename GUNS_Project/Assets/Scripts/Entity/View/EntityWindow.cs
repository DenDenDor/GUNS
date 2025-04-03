using UnityEngine;

public abstract class EntityWindow : AbstractWindowUi
{
    public override void Init()
    {
        
    }

    protected AbstractEntity Add(AbstractEntity entity)
    {
        EntityController.Instance.AddEntity(entity);

        return entity;
    }
}