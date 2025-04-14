using System.Collections;
using System.Linq;
using UnityEngine;

public class BattleRouter : IRouter
{
    private Coroutine _coroutine;

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
        PressurePlateController.Instance.AddPressurePlate(AllyPoint.AttackButton, PressurePlateType.FillingUp);
        
        Plate.UpdateBar(0);

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
            CoroutineController.Instance.StopCoroutine(_coroutine);
        
            Plate.UpdateBar(0);
        }
    }
    
    private IEnumerator Cooldown()
    {
        float fillness = 0;
        float time = 0;
        
        while (time < 1)
        {
            time += Time.deltaTime;
            
            Plate.UpdateBar(time);

            yield return null;
        }
        
        BattleController.Instance.StartMoving();
    }

    private void OnEntered(AbstractPressurePlateView view)
    {
        if (BattleController.Instance.IsMoving == false)
        {
            _coroutine = CoroutineController.Instance.StartCoroutine(Cooldown());
        }
    }
    
    private void OnRestarted()
    {
        BattleController.Instance.StopMoving();

        Plate.UpdateBar(0);
    }


    public void Exit()
    {
        EntityController.Instance.Removed -= OnRemoved;
        Plate.Entered -= OnEntered;
    }
}