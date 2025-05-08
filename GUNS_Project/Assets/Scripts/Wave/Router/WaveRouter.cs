using System.Linq;
using UnityEngine;

public class WaveRouter : IRouter
{
    private FlagView _prefab;
    private Vector3 _enemyPosition;
    private WaveWindow Window => UiController.Instance.GetWindow<WaveWindow>();

    public void Init()
    {
        _prefab = FactoryController.Instance.FindPrefab<FlagView>();
        
        WaveController.Instance.UpdateWave(Window.Waves.FirstOrDefault());


        
        EntityController.Instance.Removed += OnRemoved;
    }

    private void OnRemoved()
    {
        int count = EntityController.Instance.Enemies.Count;
        
        if (count == 1)
        {
            _enemyPosition = EntityController.Instance.Enemies[0].transform.position;
        }
        
        if (count == 0)
        {
            Debug.Log("YOU WIN!");
            
            UpgradeController.Instance.StoppedUpgraded += Upgraded;
            
            FlagView flagView = Window.Create(_prefab, _enemyPosition);

            flagView.Entered += OnEnter;

        }
    }

    private void OnEnter()
    {
        WaveController.Instance.WinWave();
    }

    private void Upgraded()
    {
        WaveController.Instance.UpdateWave(Window.Waves.LastOrDefault());
        
        UpgradeController.Instance.StoppedUpgraded -= Upgraded;
    }

    public void Exit()
    {
        
    }
}