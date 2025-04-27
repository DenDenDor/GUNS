using UnityEngine;

public class GoldPickUp : AbstractCurrencyPickUp
{
   [field: SerializeField]  protected override Color CurrencyColor { get; set; }
}
