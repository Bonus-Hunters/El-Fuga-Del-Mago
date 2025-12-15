using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HoldToSkip : MonoBehaviour
{
    [Header("Skip Settings")]
    public float holdDuration = 2.5f;
    public string nextSceneName = "Level_1";

    [Header("UI")]
    public Image skipFillImage;

    private float holdTimer = 0f;
    private bool skipped = false;

    void Update()
    {
        if (skipped) return;

        if (Input.GetKey(KeyCode.Space))
        {
            holdTimer += Time.deltaTime;
            skipFillImage.fillAmount = holdTimer / holdDuration;

            if (holdTimer >= holdDuration)
            {
                Skip();
            }
        }
        else
        {
            // Reset when released
            holdTimer = 0f;
            skipFillImage.fillAmount = 0f;
        }
    }

    void Skip()
    {
        skipped = true;
        SceneManager.LoadScene(nextSceneName);
    }
}
