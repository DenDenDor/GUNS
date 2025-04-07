using TMPro;
using UnityEngine;

public class GoldPressurePlateView : AbstractPressurePlateView
{
   [SerializeField] private TextMeshProUGUI _text;

   public void UpdatePrice(int amount)
   {
      _text.text = amount.ToString();
   }
}
