using UnityEngine;

public class AttackRouter : IRouter
{
    public void Init()
    {
        EntityController.Instance.Added += OnAdd;

        //UpdateController.Instance.Add(OnUpdate);
    }

    private void OnAdd(AbstractEntity obj)
    {
        
    }

    private void OnUpdate()
    {
        foreach (var entity in EntityController.Instance.Entities)
        {
            Vector3 currentPosition = entity.transform.position;

            AbstractEntity nearestAlly = null;
            float minDistanceSqr = 25f; // 5 squared for distance comparison

            if (entity is EnemyView)
            {
            }
            else if (entity is SoldierView)
            {
                
            }
        }
    }

    public void Exit()
    {
        EntityController.Instance.Added -= OnAdd;
    }
}