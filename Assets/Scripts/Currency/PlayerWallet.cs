using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWallet : MonoBehaviour
{
    [System.Serializable]
    private class CurrencyAmount
    {
        public CurrencyData currencyData;
        public int amount;
    }

    [SerializeField] private List<CurrencyAmount> currencies = new();

    /// <summary>
    /// Add currency to wallet
    /// </summary>
    public void AddCurrency(CurrencyData data, int value)
    {
        var currency = currencies.Find(c => c.currencyData == data);
        if (currency != null)
        {
            currency.amount += value;
            if (currency.amount > data.maxAmount)
                currency.amount = data.maxAmount;
        }
        else
        {
            currencies.Add(new CurrencyAmount { currencyData = data, amount = Mathf.Min(value, data.maxAmount) });
        }
        // Show popup
        CurrencyManager.Instance.ShowPopup($"+{value} {data.currencyName}", Color.green);

        Debug.Log($"Added {value} {data.currencyName}. Current: {GetAmount(data)}");
    }

    /// <summary>
    /// Spend currency if enough is available
    /// </summary>
    public bool SpendCurrency(CurrencyData data, int value)
    {
        var currency = currencies.Find(c => c.currencyData == data);
        if (currency != null && currency.amount >= value)
        {
            currency.amount -= value;

            CurrencyManager.Instance.ShowPopup($"-{value} {data.currencyName}", Color.red);

            Debug.Log($"Spent {value} {data.currencyName}. Remaining: {currency.amount}");
            return true;
        }
        Debug.Log($"Not enough {data.currencyName} to spend {value}");
        return false;
    }

    /// <summary>
    /// Get current amount
    /// </summary>
    public int GetAmount(CurrencyData data)
    {
        var currency = currencies.Find(c => c.currencyData == data);
        return currency != null ? currency.amount : 0;
    }
}
