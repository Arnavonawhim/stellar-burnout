using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoBGMPlayer : MonoBehaviour
{
    public VideoClip[] clips; // assign your 3 mp4 files
    private VideoPlayer vp;

    void Start()
    {
        vp = gameObject.AddComponent<VideoPlayer>();
        vp.playOnAwake = false;
        vp.audioOutputMode = VideoAudioOutputMode.AudioSource;

        var audio = gameObject.AddComponent<AudioSource>();
        vp.SetTargetAudioSource(0, audio);

        StartCoroutine(PlayAll());
    }

    IEnumerator PlayAll()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            vp.clip = clips[i];
            vp.Play();
            // wait until the clip finishes
            while (vp.isPlaying)
                yield return null;
        }
    }
}
