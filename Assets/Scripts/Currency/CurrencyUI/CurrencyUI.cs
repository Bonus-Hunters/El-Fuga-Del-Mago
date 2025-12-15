using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private PlayerWallet playerWallet;

    [System.Serializable]
    private class CurrencyDisplay
    {
        public CurrencyData currencyData;
        public TMP_Text uiText; // Use TMP_Text if using TextMeshPro
    }

    [SerializeField] private List<CurrencyDisplay> displays = new List<CurrencyDisplay>();

    private void Start()
    {
        if (playerWallet == null)
        {
            Debug.LogError("Assign PlayerWallet to CurrencyUI!");
            return;
        }

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {

    }
}
