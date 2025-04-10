using System.Linq;
using UnityEngine;

public class WaveRouter : IRouter
{
    private WaveWindow Window => UiController.Instance.GetWindow<WaveWindow>();

    public void Init()
    {
        WaveController.Instance.UpdateWave(Window.Waves.FirstOrDefault());
    }

    public void Exit()
    {
        
    }
}