using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;

public class SoundsSettingsUI : MonoBehaviour
{
    public GameObject Container;
    public GameObject PauseMenuContainer;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void onBack()
    {
        Container.SetActive(false);
        PauseMenuContainer.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}
