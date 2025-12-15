using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;



    void Start()
    {
        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
        videoPlayer.loopPointReached += OnVideoFinished;
    }
    void OnPrepared(VideoPlayer vp)
    {
        vp.Play();
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}