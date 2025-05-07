using System.Collections.Generic;
using UnityEngine;

public class UpgradedStatPanel : MonoBehaviour
{
   [SerializeField] private List<UpgradedStatView> _views;

   public List<UpgradedStatView> Views => _views;
}
