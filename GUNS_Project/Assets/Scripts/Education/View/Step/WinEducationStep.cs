using System.Collections;
using UnityEngine;

public class WinEducationStep : AbstractEducationStep
{
   private FlagView _flagView;
   private bool _isActive;
   
   protected override void OnOpen()
   {
   //   StopCoroutine(Wait());
      _isActive = true;
   }

   private IEnumerator Wait()
   {
      yield return new WaitForSeconds(0.5f);
      
    //  UiController.Instance.GetWindow<WaveWindow>().CreatedFlag += OnCreateFlag;
   }

   protected override void OnUpdate()
   {
      base.OnUpdate();

      if (_isActive)
      {
         _flagView = FindObjectOfType<FlagView>();

         if (_flagView != null)
         {
            EnterArrow(GetTarget);

            _flagView.Entered += Entered;

            Debug.LogError("FOUND " + _flagView);
            
            _isActive = false;
         }
      }
   }

   private Transform GetTarget()
   {
      return _flagView.transform;
   }

   // private void OnCreateFlag()
   // {
   //    _flagView = UiController.Instance.GetWindow<WaveWindow>().Flag;
   //    
   //    Debug.LogError("FOUND " + _flagView);
   //
   //    _flagView.Entered += Entered;
   //    
   //    EnterArrow(GetTarget);
   // }

   private void Entered()
   {
      Debug.LogError("FLAG CREATED");
      Close();

      _flagView.Entered -= Entered;
   }

   protected override void OnClose()
   {
     // UiController.Instance.GetWindow<WaveWindow>().CreatedFlag -= OnCreateFlag;
      ExitArrow();
   }
}
