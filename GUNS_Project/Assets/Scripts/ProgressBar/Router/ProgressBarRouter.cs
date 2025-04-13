using UnityEngine;

public class ProgressBarRouter : IRouter
{
    private ProgressBarWindow Window => UiController.Instance.GetWindow<ProgressBarWindow>();

    private int _maxEnemies;

    private ProgressBarView _view;
    
    public void Init()
    {
        ProgressBarView prefab = FactoryController.Instance.FindPrefab<ProgressBarView>();
        
        _view = Window.Create(prefab);
        
        EntityController.Instance.Removed += OnRemoved;
        
        WaveController.Instance.StartedNewWave += OnStartedNewWave;
    }

    private void OnStartedNewWave()
    {
        _maxEnemies = EntityController.Instance.Enemies.Count;
        
        _view.UpdateBar(GenerateValue());
    }

    private void OnRemoved()
    {
        _view.UpdateBar(GenerateValue());
    }

    private float GenerateValue()
    {
        int leftEnemies = _maxEnemies - EntityController.Instance.Enemies.Count;
        
        return (float) leftEnemies / _maxEnemies;
    }
    public void Exit()
    {
        
    }
}