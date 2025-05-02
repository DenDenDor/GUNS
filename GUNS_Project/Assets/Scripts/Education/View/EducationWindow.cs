using System;
using TMPro;
using UnityEngine;

public class EducationWindow : AbstractWindowUi
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private AbstractEducationStep[] _steps; 
    
    public event Action Finished;

    public int StepsCount => _steps.Length;

    public override void Init()
    {
        _steps = GetComponentsInChildren<AbstractEducationStep>();
    }

    public void StartStep(int educationStep)
    {
        AbstractEducationStep step = _steps[educationStep];
            
        step.Open();

        UpdateText(step.Key);
        
        step.Finished += OnFinished;
    }

    private void OnFinished(AbstractEducationStep step)
    {
        Finished?.Invoke();
        
        step.Finished -= OnFinished;
    }

    private void UpdateText(string text)
    {
        _text.text = text;
    }
}