using UnityEngine;
using UnityEngine.Profiling;

public class PerformanceHUD : MonoBehaviour
{
    [Header("FPS Settings")]
    [SerializeField] private int frameRange = 60;
    private int frameCount = 0;
    private float deltaTime = 0.0f;
    private float fps = 0.0f;

    private GUIStyle labelStyle;
    private Texture2D backgroundTexture;

    private void Awake()
    {
        // Create a simple background texture
        backgroundTexture = new Texture2D(1, 1);
        backgroundTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.8f)); // semi-transparent black
        backgroundTexture.Apply();

        // Create a GUIStyle
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 14;
        labelStyle.normal.textColor = Color.white;
        labelStyle.padding = new RectOffset(5, 5, 5, 5);
    }

    private void Update()
    {
        // --- Calculate FPS ---
        frameCount++;
        deltaTime += Time.unscaledDeltaTime;
        if (frameCount >= frameRange)
        {
            fps = frameCount / deltaTime;
            frameCount = 0;
            deltaTime = 0;
        }
    }

    private void OnGUI()
    {
        float ms = Time.deltaTime * 1000f; // frame time in ms
        long totalMemory = Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024); // MB
        float cpuUsage = Profiler.GetMonoUsedSizeLong() / (1024f * 1024f); // MB

        // Starting position
        float x = 10f;
        float y = 10f;
        float width = 220f;
        float height = 25f;
        float spacing = 5f;

        // Draw FPS
        DrawLabel(new Rect(x, y, width, height), $"FPS: {fps:F1} ({ms:F1} ms)", Color.green);
        y += height + spacing;

        // Draw Memory Usage
        DrawLabel(new Rect(x, y, width, height), $"Memory Usage: {totalMemory} MB", Color.yellow);
        y += height + spacing;

        // Draw CPU Memory
        DrawLabel(new Rect(x, y, width, height), $"CPU Memory: {cpuUsage:F1} MB", Color.cyan);
        y += height + spacing;
    }

    private void DrawLabel(Rect rect, string text, Color textColor)
    {
        // Draw background
        GUI.color = Color.white;
        GUI.DrawTexture(rect, backgroundTexture);

        // Draw text
        labelStyle.normal.textColor = textColor;
        GUI.Label(rect, text, labelStyle);
    }
}
