using UnityEngine;

public class ProgressBarRouter : IRouter
{
    private ProgressBarWindow Window => UiController.Instance.GetWindow<ProgressBarWindow>();

    private int _maxEnemies;

    private ProgressBarView _view;
    
    public void Init()
    {
        ProgressBarView prefab = Resources.Load<ProgressBarView>("Prefabs/ProgressBarView");
        
        _view = Window.Create(prefab);

        _maxEnemies = EntityController.Instance.Enemies.Count;
        
        _view.UpdateBar(GenerateValue());
        
        EntityController.Instance.Removed += OnRemoved;
    }

    private void OnRemoved()
    {
        _view.UpdateBar(GenerateValue());
    }

    private float GenerateValue()
    {
        return ((float) _maxEnemies - EntityController.Instance.Enemies.Count) / _maxEnemies;
    }
    public void Exit()
    {
        
    }
}