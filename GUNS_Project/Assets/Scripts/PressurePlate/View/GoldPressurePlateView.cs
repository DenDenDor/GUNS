using TMPro;
using UnityEngine;

public class GoldPressurePlateView : AbstractPressurePlateView
{
   [SerializeField] private TextMeshProUGUI _text;

   public void UpdateCount(int amount)
   {
      _text.text = amount.ToString();
   }
}
