using UnityEngine;

public class testing : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public CurrencyData goldCurrency;
    public CurrencyData magicShards;

    void Start()
    {
        //if (currencyManager == null)
        //{
        //    Debug.LogError("CurrencyManager reference not assigned!");
        //    return;
        //}

        //// Test adding currency
        //Debug.Log("Testing AddCurrency...");
        //currencyManager.playerWallet.AddCurrency(goldCurrency, 50);
        //currencyManager.playerWallet.AddCurrency(magicShards, 25);

        //// Test spending currency
        //Debug.Log("Testing SpendCurrency...");
        //currencyManager.playerWallet.SpendCurrency(goldCurrency, 20);
        //currencyManager.playerWallet.SpendCurrency(magicShards, 30); // not enough, should fail
        CurrencyManager.Instance.ShowPopup("+10 Gold", Color.yellow);

    }

    void Update()
    {
        // Optional: Press keys to test dynamically
        if (Input.GetKeyDown(KeyCode.G))
        {
            currencyManager.playerWallet.AddCurrency(goldCurrency, 10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currencyManager.playerWallet.SpendCurrency(goldCurrency, 5);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            currencyManager.playerWallet.AddCurrency(magicShards, 10);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            currencyManager.playerWallet.SpendCurrency(magicShards, 5);
        }
    }
}
