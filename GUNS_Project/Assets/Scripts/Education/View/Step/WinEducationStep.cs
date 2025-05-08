using UnityEngine;

public class WinEducationStep : AbstractEducationStep
{
   private FlagView _flagView;
   
   protected override void OnOpen()
   {
      UiController.Instance.GetWindow<WaveWindow>().CreatedFlag += OnCreateFlag;

      EnterArrow(GetTarget);
   }
   
   private Transform GetTarget()
   {
      return _flagView.transform;
   }

   private void OnCreateFlag()
   {
      _flagView = UiController.Instance.GetWindow<WaveWindow>().Flag;
      
      _flagView.Entered += Entered;
   }

   private void Entered()
   {
      Close();

      _flagView.Entered -= Entered;
   }

   protected override void OnClose()
   {
      UiController.Instance.GetWindow<WaveWindow>().CreatedFlag -= OnCreateFlag;
      ExitArrow();
   }
}
