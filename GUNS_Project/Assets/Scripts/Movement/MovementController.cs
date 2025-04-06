using UnityEngine;
using System;

public class MovementController : MonoBehaviour
{
    private static MovementController _instance;

    public static MovementController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MovementController>();

                if (_instance == null)
                {
                    throw new NotImplementedException("MovementController not found!");
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

    public void UpdateMovement(AbstractEntity entity, IMovement movement)
    {
        EntityController.Instance.FullEntities[entity].Movement = movement;
    }
}