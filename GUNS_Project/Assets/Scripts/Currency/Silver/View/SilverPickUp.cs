using UnityEngine;

public class SilverPickUp : AbstractCurrencyPickUp
{
    [field: SerializeField]  protected override Color CurrencyColor { get; set; }
}
