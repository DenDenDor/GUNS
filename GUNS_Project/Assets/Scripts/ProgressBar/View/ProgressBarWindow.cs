using UnityEngine;

public class ProgressBarWindow : AbstractFactoryWindow
{
    [SerializeField] private Transform _point;
    
    public override void Init()
    {
        
    }

    public ProgressBarView Create(ProgressBarView prefab)
    {
        return CreatePrefab(prefab, _point, true);
    }
}