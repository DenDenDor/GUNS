using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaneRouter : IRouter
{
     private Coroutine _coroutine;
     private PlaneView _prefabPlane;
     private PlaneBombView _prefabBomb;
     private PlaneView _createdPlane;
     private bool _isWorking = true;
     private float _currentFillness = 0f;
     private const float FILL_TIME = 2f; // Время полного заполнения в секундах

     private float _time = 0;
     private Dictionary<PlaneBombView, IEnumerable<EnemyView>> _pointByEnemies = new();
     private bool _isCooldown = true;

     private PlaneWindow Window => UiController.Instance.GetWindow<PlaneWindow>();

     private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;

     private JetPlate _jetPlate;

     private AbstractPressurePlateView Plate =>
        PressurePlateController.Instance.PressurePlateViewsByPoints[AllyPoint.JetPoint];

     public void Init()
    {
        _prefabPlane = FactoryController.Instance.FindPrefab<PlaneView>();
        _prefabBomb = FactoryController.Instance.FindPrefab<PlaneBombView>();

        WaveController.Instance.StartedNewWave += OnStartNewWave;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private int _droppedBombs;

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
                _jetPlate.UpdateText(GenerateTime);
                _seconds = 0;
            }

            if (_time > _waitTime)
            {
                _isWorking = true;
                _time = 0;
                _seconds = 1;
                
                _jetPlate.UpdateText(null);
                
                _coroutine = CoroutineController.Instance.StartCoroutine(EmptyBar());
            }
        }
        if (_createdPlane != null && _isCooldown)
        {
            IEnumerable<EnemyView> enemies = EntityController.Instance.Enemies.Where(x =>
                Vector3.Distance(new Vector3(x.transform.position.x, 0, x.transform.position.z), new Vector3(_createdPlane.transform.position.x, 0, _createdPlane.transform.position.z)) < 4);

            var enemyViews = enemies as EnemyView[] ?? enemies.ToArray();
                

            if (enemyViews.Any())
            {
                CoroutineController.Instance.RunCoroutine(Wait(enemyViews));
            }
        }
    }

    private IEnumerator Wait(EnemyView[] enemies)
    {
        _isCooldown = false;
        PlaneBombView created = Window.CreateBomb(_prefabBomb, _createdPlane.Point);
        
        _pointByEnemies.Add(created, enemies);
        
        created.Entered +=  CreatedOnEntered;
        _droppedBombs++;
        yield return new WaitForSeconds(2);

        if (_droppedBombs > 3)
        {
            _createdPlane.StopMoving();
            MovementController.Instance.StartMoving();
            Window.RemovePlane();
        }
        else
        {
            _isCooldown = true;
        }
    }

    private void CreatedOnEntered(PlaneBombView point)
    {
        foreach (var enemy in EntityController.Instance.Enemies.Where(x =>
                     Vector3.Distance(new Vector3(x.transform.position.x, 0, x.transform.position.z), new Vector3(point.transform.position.x, 0, point.transform.position.z)) < 3))
        {
            Debug.Log("ENEMY! ! ! " + enemy);

            HealthController.Instance.GetByEntity(enemy).TakeDamage(50);
        }

        point.Entered -= CreatedOnEntered;

        _pointByEnemies.Remove(point);
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
        _droppedBombs = 0;
        SubscribePlate();
    }

    private void SubscribePlate()
    {
        PressurePlateController.Instance.AddPressurePlate(AllyPoint.JetPoint, PressurePlateType.Timer, BuildingType.Plane);
        
        Plate.UpdateBar(0);

        _jetPlate = Plate.GetComponent<JetPlate>();

        Plate.Entered += OnEntered;
        Plate.Exited += OnExited;
        
        _createdPlane = Window.Create(_prefabPlane, Plate.transform.position);
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
        
        Window.SetForCamera(_createdPlane);

        _isWorking = false;
        
        _createdPlane.StartMoving();

        MovementController.Instance.StopMoving();

    }
    
    private void UpdateBar(float currentFillness)
    {
        Plate.UpdateBar(currentFillness);
        _jetPlate.UpdateBar(currentFillness);
    }

    private void OnEntered(AbstractPressurePlateView view)
    {
        if (_coroutine != null)
        {
            CoroutineController.Instance.StopCoroutine(_coroutine);
        }

        _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown(view));
    }


    public void Exit()
    {
        Plate.Entered -= OnEntered;
    }
}