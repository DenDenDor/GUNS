using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombRouter : IRouter
{
     private Coroutine _coroutine;
     private BombView _prefab;
     private bool _isWorking = true;

     private float _time = 0;
     private float _currentFillness = 0f;
     private const float FILL_TIME = 2f; // Время полного заполнения в секундах
     private BombWindow Window => UiController.Instance.GetWindow<BombWindow>();

     private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;


     private BombPlate _bombPlate;
     
     private AbstractPressurePlateView Plate =>
        PressurePlateController.Instance.PressurePlateViewsByPoints[AllyPoint.BombPoint];

     public void Init()
    {
        _prefab = FactoryController.Instance.FindPrefab<BombView>();

        WaveController.Instance.StartedNewWave += OnStartNewWave;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private float _seconds = 1;
    private int _waitTime = 10;
    
    private void OnUpdate()
    {
        if (_isWorking == false)
        {
            _time += Time.deltaTime;
            _seconds += Time.deltaTime;

            if (_seconds > 1)
            {
                _bombPlate.UpdateText(GenerateTime);
                _seconds = 0;
            }

            if (_time > _waitTime)
            {
                _isWorking = true;
                _time = 0;
                _seconds = 1;
                
                _bombPlate.UpdateText(null);
                
                _coroutine = CoroutineController.Instance.StartCoroutine(EmptyBar());
            }
        }
    }

    private string GenerateTime()
    {
        int totalSeconds = _waitTime - (int) _time;
        
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        string timeString = $"{minutes}:{seconds:D2}";

        return timeString;
    }


    private void OnStartNewWave()
    {
        SubscribePlate();
    }

    private void SubscribePlate()
    {
        PressurePlateController.Instance.AddPressurePlate(AllyPoint.BombPoint, PressurePlateType.Timer, BuildingType.Bomb);
        
        Plate.UpdateBar(0);

        _bombPlate = Plate.GetComponent<BombPlate>();

        Plate.Entered += OnEntered;
        Plate.Exited += OnExited;
    }


    private void OnExited()
    {
        if (_coroutine != null)
        {
            if (_coroutine != null)
            {
                CoroutineController.Instance.StopCoroutine(_coroutine);
                _coroutine = null;
            }
            
            // Плавно убираем заполнение при выходе
            _coroutine = CoroutineController.Instance.StartCoroutine(EmptyBar());
        }
    }
    
    private IEnumerator EmptyBar()
    {
        while (_currentFillness > 0f)
        {
            _currentFillness -= Time.deltaTime / (FILL_TIME * 0.5f); // Убираем заполнение в 2 раза быстрее
            UpdateBar(_currentFillness);
            yield return null;
        }
        
        _currentFillness = 0f;
        UpdateBar(0f);
        _coroutine = null;
    }

    private IEnumerator Cooldown(AbstractPressurePlateView view)
    {
        if (_isWorking == false)
        {
            yield break;
        }
        
        while (_currentFillness < 1f)
        {
            _currentFillness += Time.deltaTime / FILL_TIME;
            UpdateBar(_currentFillness);
            yield return null;
        }
 
        BombView bomb = Window.Create(_prefab, view.transform.position);

        Transform[] points = EntityController.Instance.Enemies.Select(x => x.transform).ToArray();
            
        bomb.UpdateTarget(GetNearestEnemy(bomb.transform, points));

        bomb.Fallen += OnFallen;
        _isWorking = false;
        MovementController.Instance.StopMoving();
        
        _coroutine = null;

        // float fillness = 0;
        // float time = 0;
        //
        // while (time < 1 && _isWorking)
        // {
        //     time += Time.deltaTime;
        //     
        //
        //     yield return null;
        // }
    }

    private void UpdateBar(float currentFillness)
    {
        Plate.UpdateBar(currentFillness);
        _bombPlate.UpdateBar(currentFillness);
    }

    private void OnFallen(BombView obj)
    {
        MovementController.Instance.StartMoving();

        foreach (var enemy in EntityController.Instance.Enemies.Where(x =>
                     Vector3.Distance(x.transform.position, obj.transform.position) < 6))
        {
            Window.RemoveBomb();
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
        if (_coroutine != null)
        {
            CoroutineController.Instance.StopCoroutine(_coroutine);
        }

        _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown(view));
        Debug.Log("ON ENTER B O M B ! ! ! ");
    }


    public void Exit()
    {
        Plate.Entered -= OnEntered;
    }
}