using System.Collections;
using System.Linq;
using UnityEngine;

public class BattleRouter : IRouter
{
    private AttackPlate _attackPlate;
    private Coroutine _coroutine;
    private float _currentFillness = 0f;
    private const float FILL_TIME = 2f; // Время полного заполнения в секундах

    private AllyPoint AllyPoint => WaveController.Instance.GenerateWaveInfo().AllyPoint;

    private AbstractPressurePlateView Plate =>
        PressurePlateController.Instance.PressurePlateViewsByPoints[AllyPoint.AttackButton];
    
    public void Init()
    {
        EntityController.Instance.Removed += OnRemoved;
        WaveController.Instance.StartedNewWave += OnStartNewWave;
    }
    
    private void OnStartNewWave()
    {
        SubscribePlate();
    }
    
    private void SubscribePlate()
    {
        PressurePlateController.Instance.AddPressurePlate(AllyPoint.AttackButton, PressurePlateType.FillingUp, BuildingType.Attack);
        _attackPlate = Plate.GetComponent<AttackPlate>();
        UpdateBar(0);
        
        Plate.Entered += OnEntered;
        Plate.Exited += OnExited;
    }

    private void OnRemoved()
    {
        if (EntityController.Instance.FullEntities.Keys.Count(x=>x is SoldierView) == 0 || EntityController.Instance.Enemies.Count == 0)
        {
            BattleController.Instance.Restart();
            OnRestarted();
        }
    }
    
    private void OnExited()
    {
        if (BattleController.Instance.IsMoving == false)
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
    
    private IEnumerator FillBar()
    {
        while (_currentFillness < 1f)
        {
            _currentFillness += Time.deltaTime / FILL_TIME;
            UpdateBar(_currentFillness);
            yield return null;
        }
        
        // Полное заполнение - начинаем движение
        BattleController.Instance.StartMoving();
        _coroutine = null;
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

    private void OnEntered(AbstractPressurePlateView view)
    {
        if (BattleController.Instance.IsMoving == false)
        {
            if (_coroutine != null)
            {
                CoroutineController.Instance.StopCoroutine(_coroutine);
            }
            
            _coroutine = CoroutineController.Instance.StartCoroutine(FillBar());
        }
    }
    
    private void OnRestarted()
    {
        BattleController.Instance.StopMoving();
        _currentFillness = 0f;
        UpdateBar(0);
    }

    private void UpdateBar(float fillness)
    {
        Plate.UpdateBar(fillness);
        _attackPlate.UpdateBar(fillness);
    }

    public void Exit()
    {
        EntityController.Instance.Removed -= OnRemoved;
        if (Plate != null)
        {
            Plate.Entered -= OnEntered;
            Plate.Exited -= OnExited;
        }
        
        if (_coroutine != null)
        {
            CoroutineController.Instance.StopCoroutine(_coroutine);
        }
    }
}