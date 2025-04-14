using TMPro;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _gold;
   [SerializeField] private TextMeshProUGUI _silver;

   public void UpdateGold(int gold)
   {
      _gold.text = gold.ToString();
   }   
   
   public void UpdateSilver(int silver)
   {
      _silver.text = silver.ToString();
   }
}
