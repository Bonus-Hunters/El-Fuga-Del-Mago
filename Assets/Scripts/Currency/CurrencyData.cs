using UnityEngine;

[CreateAssetMenu(fileName = "NewCurrency", menuName = "Currency/CurrencyData")]
public class CurrencyData : ScriptableObject
{
    public string currencyName;   // Example: "ORO" or "Magic Shards"
    public Sprite icon;           // UI icon
    public int maxAmount = 9999;  // Maximum the player can hold
}