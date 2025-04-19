using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombRouter : IRouter
{
     private Coroutine _coroutine;
     private BombView _prefab;
     private bool _isWorking = true;

     private BombWindow Window => UiController.Instance.GetWindow<BombWindow>();

     private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;


     private AbstractPressurePlateView Plate =>
        PressurePlateController.Instance.PressurePlateViewsByPoints[AllyPoint.BombPoint];

     public void Init()
    {
        _prefab = FactoryController.Instance.FindPrefab<BombView>();

        WaveController.Instance.StartedNewWave += OnStartNewWave;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private float _time = 0;

    private void OnUpdate()
    {
        if (_isWorking == false)
        {
            _time += Time.deltaTime;

            if (_time > 10)
            {
                _isWorking = true;
                _time = 0;
            }
        }
    }


    private void OnStartNewWave()
    {
        SubscribePlate();
    }

    private void SubscribePlate()
    {
        PressurePlateController.Instance.AddPressurePlate(AllyPoint.BombPoint, PressurePlateType.Timer, BuildingType.Bomb);
        
        Plate.UpdateBar(0);

        Plate.Entered += OnEntered;
        Plate.Exited += OnExited;
    }


    private void OnExited()
    {
        if (_coroutine != null)
        {
            CoroutineController.Instance.StopCoroutine(_coroutine);

            _coroutine = null;
        
            Plate.UpdateBar(0);

        }
    }

    private IEnumerator Cooldown(AbstractPressurePlateView view)
    {
        float fillness = 0;
        float time = 0;
        
        while (time < 1 && _isWorking)
        {
            time += Time.deltaTime;
            
            Plate.UpdateBar(time);
            BombView bomb = Window.Create(_prefab, view.transform.position);

            Transform[] points = EntityController.Instance.Enemies.Select(x => x.transform).ToArray();
            
            bomb.UpdateTarget(GetNearestEnemy(bomb.transform, points));

            bomb.Fallen += OnFallen;
            _isWorking = false;
            
            yield return null;
        }
    }

    private void OnFallen(BombView obj)
    {
        foreach (var enemy in EntityController.Instance.Enemies.Where(x =>
                     Vector3.Distance(x.transform.position, obj.transform.position) < 6))
        {
            HealthController.Instance.GetByEntity(enemy).TakeDamage(150);
        }
    }

    Transform GetNearestEnemy(Transform transform, Transform[] enemies)
    {
        Transform nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.position, currentPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }


    private void OnEntered(AbstractPressurePlateView view)
    {
        _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown(view));


        Debug.Log("ON ENTER B O M B ! ! ! ");
    }


    public void Exit()
    {
        Plate.Entered -= OnEntered;
    }
}