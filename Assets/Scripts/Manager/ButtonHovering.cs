using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;    // assign your TMP text here
    public CanvasGroup background; // optional if you want to handle transparency

    private void Start()
    {
        // Make background always transparent (if you want)
        if (background != null)
            background.alpha = 0f;

        // Set default text color
        text.color = Color.white;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.gray;  // hovered text color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white; // normal text color
    }
}
