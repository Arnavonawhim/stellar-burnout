using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string gameSceneName = "Palakkascene";

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadGame();
    }

    public void SkipVideo()
    {
        LoadGame();
    }

    void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
