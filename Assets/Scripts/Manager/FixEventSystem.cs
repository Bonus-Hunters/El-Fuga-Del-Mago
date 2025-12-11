using UnityEngine;
using UnityEngine.EventSystems;

public class FixEventSystem : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }

    void Awake()
    {
        var systems = FindObjectsOfType<EventSystem>();

        if (systems.Length > 1)
        {
            for (int i = 1; i < systems.Length; i++)
                Destroy(systems[i].gameObject);
        }
    }
}