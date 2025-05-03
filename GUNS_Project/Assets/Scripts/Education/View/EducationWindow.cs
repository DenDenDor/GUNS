using System;
using TMPro;
using UnityEngine;

public class EducationWindow : AbstractWindowUi
{
    [SerializeField] private EducationTextView _educationTextView;
    
    private AbstractEducationStep[] _steps; 
    
    public event Action Finished;

    public int StepsCount => _steps.Length;

    public override void Init()
    {
        _steps = GetComponentsInChildren<AbstractEducationStep>();

        foreach (var step in _steps)
        {
            step.Init();
        }
    }

    public void StartStep(int educationStep)
    {
        AbstractEducationStep step = _steps[educationStep];
            
        step.Open();

        UpdateText(() => step.Key.LocalizedText);
        
        step.Finished += OnFinished;
    }

    private void OnFinished(AbstractEducationStep step)
    {
        Finished?.Invoke();
        
        step.Finished -= OnFinished;
    }

    private void UpdateText(Func<string> getText)
    {
        _educationTextView.UpdateText(getText);
    }

    public void Open()
    {
        _educationTextView.Open();
    }

    public void Close()
    {
        _educationTextView.Close();
    }
}