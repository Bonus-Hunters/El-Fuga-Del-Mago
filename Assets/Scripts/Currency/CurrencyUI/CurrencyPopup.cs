using UnityEngine;
using TMPro;

public class CurrencyPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private float floatSpeed = 50f;
    [SerializeField] private float duration = 1f;

    private float timer;

    public void Setup(string text, Color color)
    {
        popupText.text = text;
        popupText.color = color;
        timer = duration;

        // Reset position (important if prefab has offset)
        transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        // Move upward
        transform.localPosition += Vector3.up * floatSpeed * Time.deltaTime;

        // Countdown
        timer -= Time.deltaTime;

        // When time is up, destroy this popup
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
