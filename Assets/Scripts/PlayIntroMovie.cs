using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayIntroMovie : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public int nextSceneIndex = 0; // default scene 0

    void Start()
    {
        // play video + audio together
        videoPlayer.Play();
        if (audioSource != null)
            audioSource.Play();

        // register callback for when video ends
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
