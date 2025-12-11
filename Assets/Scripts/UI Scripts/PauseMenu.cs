using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject mainMenuContainer;
    public GameObject soundSettingsContainer;
    bool inSoundSetting = false;

    void Start()
    {
        mainMenuContainer.SetActive(false);
        soundSettingsContainer.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Player.playerInUI)
            {
                if (inSoundSetting)
                {
                    inSoundSetting = false;
                    onBack();
                }
                ResumeButton();
                return;
            }
            Player.playerInUI = true;
            AudioListener.pause = true;
            mainMenuContainer.SetActive(true);
            Time.timeScale = 0;

        }
    }

    public void ResumeButton()
    {
        Player.playerInUI = false;
        AudioListener.pause = false;
        mainMenuContainer.SetActive(false);
        Time.timeScale = 1;

    }

    public void SoundSettings()
    {
        inSoundSetting = true;
        mainMenuContainer.SetActive(false);
        soundSettingsContainer.SetActive(true);

    }

    // should make a transition and load the first scene [intro to the game]
    // UnityEngine.SceneManagement.SceneManager.LoadScene("name of the scene");


    public void ExitButton()
    {
        Time.timeScale = 1;
        inSoundSetting = false;
        mainMenuContainer.SetActive(false);
        soundSettingsContainer.SetActive(false);
        Player.playerInUI = false;
        AudioListener.pause = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("IntroScene");
    }

    public void onBack()
    {
        inSoundSetting = false;
        soundSettingsContainer.SetActive(false);
        mainMenuContainer.SetActive(true);
    }

}
