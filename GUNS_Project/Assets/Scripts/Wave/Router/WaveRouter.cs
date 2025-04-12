using System.Linq;
using UnityEngine;

public class WaveRouter : IRouter
{
    private WaveWindow Window => UiController.Instance.GetWindow<WaveWindow>();

    public void Init()
    {
        WaveController.Instance.UpdateWave(Window.Waves.FirstOrDefault());
        
        EntityController.Instance.Removed += OnRemoved;
    }

    private void OnRemoved()
    {
        if (EntityController.Instance.Enemies.Count == 0)
        {
            Debug.Log("YOU WIN!");
            
            WaveController.Instance.UpdateWave(Window.Waves.LastOrDefault());
        }
    }

    public void Exit()
    {
        
    }
}