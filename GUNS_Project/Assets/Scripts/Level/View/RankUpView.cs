using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankUpView : MonoBehaviour
{
   [SerializeField] private Button _button;
   [SerializeField] private TextMeshProUGUI _text;

   public event Action Closed;
   
   private void Awake()
   {
      _button.onClick.AddListener(() =>
      {
         Debug.Log("CLOSED ! ! !");
         Closed?.Invoke();
      });
   }

   public void UpdateView(int level)
   {
      _text.text = level.ToString();
   }
}
