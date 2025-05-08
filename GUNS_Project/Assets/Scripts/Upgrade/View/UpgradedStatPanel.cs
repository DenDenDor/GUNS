using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradedStatPanel : MonoBehaviour
{
   [SerializeField] private List<UpgradedStatView> _views;
   [SerializeField] private Button _closeButton;
   public event Action Closed;

   private void Awake()
   {
      _closeButton.onClick.AddListener(() => Closed?.Invoke());
   }

   public List<UpgradedStatView> Views => _views;
}
