using UnityEngine;

public class LevelWindow : AbstractFactoryWindow
{
    [SerializeField] private Transform _point;
    
    public override void Init()
    {
        
    }

    public RankUpView Create(RankUpView prefabUi)
    {
        return CreatePrefab(prefabUi, _point, true);
    }
}