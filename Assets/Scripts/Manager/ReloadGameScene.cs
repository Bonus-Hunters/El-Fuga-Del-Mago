using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReloadGameScene : MonoBehaviour
{
    public static ReloadGameScene Instance;

    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1.5f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayerDied()
    {
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Freeze everything
        Time.timeScale = 0f;

        // Fade to black (unscaled time!)
        yield return StartCoroutine(Fade(1));

        // Small delay
        yield return new WaitForSecondsRealtime(0.5f);

        // Reset time BEFORE reloading
        Time.timeScale = 1f;

        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvas.alpha;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = targetAlpha;
    }
}
