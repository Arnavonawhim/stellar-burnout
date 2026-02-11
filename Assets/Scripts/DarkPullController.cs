using UnityEngine;
using UnityEngine.InputSystem;

public class DarkPullController : MonoBehaviour
{
    public CharacterSwitcher switcher;
    public Key pullKey = Key.Q;
    public float pullRadius = 2f;

    PullableObject currentObj;
    Keyboard kb;

    // 🔊 Pull Sound
    public AudioClip pullStartSound;
    public AudioClip pullLoopSound;
    private AudioSource audioSource;

    void Start()
    {
        kb = Keyboard.current;

        // Setup AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
    }

    void Update()
    {
        if (!switcher.IsDark())
        {
            if (currentObj != null) currentObj.StopPull();
            StopPullSound();
            currentObj = null;
            return;
        }

        if (kb[pullKey].wasPressedThisFrame)
        {
            TryStartPull();
        }

        if (kb[pullKey].wasReleasedThisFrame)
        {
            StopPull();
        }
    }

    void TryStartPull()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pullRadius);

        foreach (var h in hits)
        {
            PullableObject po = h.GetComponent<PullableObject>();

            if (po != null)
            {
                Debug.Log("Found Pullable: " + h.name);

                currentObj = po;
                po.StartPull(transform);

                // 🔊 Play Pull Start Sound
                PlayPullSound();

                return;
            }
        }

        Debug.Log("No pullable objects in range");
    }

    void StopPull()
    {
        if (currentObj != null)
            currentObj.StopPull();

        StopPullSound();
        currentObj = null;
    }

    // 🔊 SOUND FUNCTIONS
    void PlayPullSound()
    {
        if (pullStartSound != null)
        {
            audioSource.PlayOneShot(pullStartSound);
        }

        // Optional Loop Sound
        if (pullLoopSound != null)
        {
            audioSource.clip = pullLoopSound;
            audioSource.loop = true;
            audioSource.PlayDelayed(0.1f);
        }
    }

    void StopPullSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.loop = false;
    }
}
