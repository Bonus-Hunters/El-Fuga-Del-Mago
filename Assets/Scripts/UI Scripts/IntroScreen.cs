using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    public GameObject Container;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onNewGame()
    {
        Destroy(Container);
        // should load the first scene 
        // make sure the scene is added in the build settings
        // open File → Build Settingss >> add desired scene 
        UnityEngine.SceneManagement.SceneManager.LoadScene("ox-boss-scene");

    }
    public void onExit()
    {
        Application.Quit();
    }
    public void onContinueGame()
    {
        Container.SetActive(false);

    }
    public void onOptions() { }
}
