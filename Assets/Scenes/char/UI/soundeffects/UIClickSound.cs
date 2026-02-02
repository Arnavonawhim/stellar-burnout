using UnityEngine;

public class UIClickSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.Play();
    }
}
