using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

enum AttackState
{
    Empty,
    InProcess,
    Activate
}
public class AttackController : MonoBehaviour
{
    private readonly Dictionary<AbstractEntity, AttackState> _entitiesInCooldown = new();

    public List<AbstractEntity> Entities =>
        _entitiesInCooldown.Where(x => x.Value == AttackState.InProcess).Select(x => x.Key).ToList();
    
    private static AttackController _instance;

    public static AttackController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AttackController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("AttackController not found!");
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }
    
    public void UpdateAttack(AbstractEntity entity, IAttack attack)
    {
        if (_entitiesInCooldown.ContainsKey(entity) && (_entitiesInCooldown[entity] == AttackState.InProcess ||
                                                        _entitiesInCooldown[entity] == AttackState.Activate))
        {
            return;
        }
        
        _entitiesInCooldown[entity] = AttackState.InProcess;

        EntityController.Instance.FullEntities[entity].Attack = attack;
    }

    public void Attack(AbstractEntity entity)
    {
        if (_entitiesInCooldown.ContainsKey(entity) && (_entitiesInCooldown[entity] == AttackState.Activate))
            return;

        _entitiesInCooldown[entity] = AttackState.Activate;
        
        CoroutineController.Instance.RunCoroutine(Cooldown(entity));
    }
    
    //private bool IsInCooldown(AbstractEntity entity) => _entitiesInCooldown.ContainsKey(entity) && _entitiesInCooldown[entity];
    
    private IEnumerator Cooldown(AbstractEntity entity)
    {
        yield return new WaitForSeconds(1);

        _entitiesInCooldown[entity] = AttackState.Empty;
        
        Debug.Log("EMPTY " + _entitiesInCooldown[entity]);
    }
    
}