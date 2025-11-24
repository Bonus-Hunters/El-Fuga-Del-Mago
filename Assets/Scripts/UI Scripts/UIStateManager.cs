using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.LowLevel;

public class UIStateController : MonoBehaviour
{
    public static UIStateController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void EnterUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable player look (replace with your camera script name)
        Camera.main.GetComponent<Player>().enabled = false;
    }

    public void ExitUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Camera.main.GetComponent<Player>().enabled = true;
    }
}
