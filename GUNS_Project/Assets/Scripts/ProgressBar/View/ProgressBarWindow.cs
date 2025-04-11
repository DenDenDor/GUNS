using UnityEngine;

public class ProgressBarWindow : AbstractWindowUi
{
    [SerializeField] private Transform _point;
    
    public override void Init()
    {
        
    }

    public ProgressBarView Create(ProgressBarView prefab)
    {
        return Instantiate(prefab, _point);
    }
}