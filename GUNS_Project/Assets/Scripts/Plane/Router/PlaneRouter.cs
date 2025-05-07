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

     private float _time = 0;
     private Dictionary<PlaneBombView, IEnumerable<EnemyView>> _pointByEnemies = new();
     private bool _isCooldown = true;

     private PlaneWindow Window => UiController.Instance.GetWindow<PlaneWindow>();

     private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;


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

    private void OnUpdate()
    {
        if (_isWorking == false)
        {
            _time += Time.deltaTime;

            if (_time > 150)
            {
                _isWorking = true;
                _time = 0;
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

    private void OnStartNewWave()
    {
        _droppedBombs = 0;
        SubscribePlate();
    }

    private void SubscribePlate()
    {
        PressurePlateController.Instance.AddPressurePlate(AllyPoint.JetPoint, PressurePlateType.Timer, BuildingType.Plane);
        
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
            _createdPlane = Window.Create(_prefabPlane, view.transform.position);
            _createdPlane.StartMoving();

            _isWorking = false;
            
            MovementController.Instance.StopMoving();
            
            yield return null;
        }
    }

    private void OnEntered(AbstractPressurePlateView view)
    {
        _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown(view));
    }


    public void Exit()
    {
        Plate.Entered -= OnEntered;
    }
}