using System.Collections;
using UnityEngine;

public class EducationRouter : IRouter
{
    private EducationWindow Window => UiController.Instance.GetWindow<EducationWindow>();

    public void Init()
    {
        if (SDKMediator.Instance.GenerateSaveData().IsEducationFinished == false)
        {
            Window.StartStep(SDKMediator.Instance.GenerateSaveData().EducationStep);
            
            Window.Open();
            
            SendMetrica.Send((EventMetricaKeys) SDKMediator.Instance.GenerateSaveData().EducationStep);
        
            Window.Finished += OnFinished;
        }
    }

    private void OnFinished()
    {
        CoroutineController.Instance.RunCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        Window.Close();

        int step = SDKMediator.Instance.GenerateSaveData().EducationStep + 1;
        
        SendMetrica.Send((EventMetricaKeys) step);
        
        yield return new WaitForSeconds(2);

        Window.Open();

        SDKMediator.Instance.SaveEducationStep(step);

        if (step >= Window.StepsCount)
        {
            SDKMediator.Instance.SaveIsEducationFinished(true);
        }
        else
        {
            Window.StartStep(step);
        }
    }

    public void Exit()
    {
        
    }
}