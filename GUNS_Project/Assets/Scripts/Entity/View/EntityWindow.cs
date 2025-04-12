using UnityEngine;

public abstract class EntityWindow : AbstractWindowUi
{
    [SerializeField] private float _health = 50;
    [SerializeField] private float _damage = 25;

    public float Health => _health;

    public float Damage => _damage;

    public override void Init()
    {
        
    }

    protected AbstractEntity Add(AbstractEntity entity, EntityModel model)
    {
        EntityController.Instance.AddEntity(entity, model);

        return entity;
    }
}