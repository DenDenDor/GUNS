using System;
using UnityEngine;
using UnityEngine.UI;

public class HealHealthView : MonoBehaviour
{
 [SerializeField] private Button _cross;
 [SerializeField] private Button[] _accepts;
 
 public event Action Accepted;
 
 public event Action Exited;

 private void Awake()
 {
  _cross.onClick.AddListener(() => Exited?.Invoke());

  foreach (var button in _accepts)
  {
   button.onClick.AddListener(OnAccept);
  }
 }

 private void OnAccept()
 {
  Accepted?.Invoke();
 }
}
