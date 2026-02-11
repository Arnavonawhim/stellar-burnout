using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum Type { Light, Dark }
    public Type type;

    [Header("Collect Sound")]
    public AudioClip collectSound;
    public float soundVolume = 1f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Hit: " + other.name);

        if (type == Type.Light && other.CompareTag("LightPlayer"))
        {
            PlayCollectSound();
            CollectibleManager.instance.CollectLight();
            Destroy(gameObject);
        }
        else if (type == Type.Dark && other.CompareTag("DarkPlayer"))
        {
            PlayCollectSound();
            CollectibleManager.instance.CollectDark();
            Destroy(gameObject);
        }
    }

    void PlayCollectSound()
    {
        if (collectSound == null) return;

        GameObject temp = new GameObject("TempAudio");
        AudioSource audio = temp.AddComponent<AudioSource>();

        audio.clip = collectSound;
        audio.volume = soundVolume;

        // ✅ Always audible
        audio.spatialBlend = 0f;

        audio.Play();
        Destroy(temp, collectSound.length);
    }
}
