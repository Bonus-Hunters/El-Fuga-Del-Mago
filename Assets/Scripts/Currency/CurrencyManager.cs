using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    public PlayerWallet playerWallet;

    [Header("UI")]
    public CurrencyPopup popupPrefab;
    public Canvas uiCanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowPopup(string text, Color color)
    {
        if (popupPrefab == null || uiCanvas == null)
            return;

        // Create a new popup every time
        Debug.Log("Showing currency popup: " + text);
        CurrencyPopup popup = Instantiate(popupPrefab, uiCanvas.transform);
        popup.Setup(text, color);
    }
}
