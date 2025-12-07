using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject Container;
    public GameObject soundSettingsContainer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Container.SetActive(true);
            Time.timeScale = 0;

        }
    }

    public void ResumeButton()
    {

        Container.SetActive(false);
        Time.timeScale = 1;

    }

    public void SoundSettings()
    {

        Container.SetActive(false);
        soundSettingsContainer.SetActive(true);

    }

    // should make a transition and load the first scene [intro to the game]
    // UnityEngine.SceneManagement.SceneManager.LoadScene("name of the scene");


    public void ExitButton()
    {
        Destroy(Container);
        UnityEngine.SceneManagement.SceneManager.LoadScene("IntroScene");
        Time.timeScale = 1;
    }

}
