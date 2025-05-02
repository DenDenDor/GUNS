using UnityEngine;

public class EducationRouter : IRouter
{
    private EducationWindow Window => UiController.Instance.GetWindow<EducationWindow>();

    public void Init()
    {
        if (SDKMediator.Instance.GenerateSaveData().IsEducationFinished == false)
        {
            Window.StartStep(SDKMediator.Instance.GenerateSaveData().EducationStep);
        
            Window.Finished += OnFinished;
        }
    }

    private void OnFinished()
    {
        int step = SDKMediator.Instance.GenerateSaveData().EducationStep + 1;
        
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