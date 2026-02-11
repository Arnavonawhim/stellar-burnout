using UnityEngine;
using System.Collections;

public class DarkDestructible : MonoBehaviour
{
    public float pulseSpeed = 4f;
    public float pulseTime = 0.5f;
    public float shrinkSpeed = 2f;

    Renderer rend;
    Material mat;
    Color baseColor;

    public bool IsDestroyed { get; private set; }
    bool pulsing;

    // 🔊 Only one sound
    public AudioClip destroySound;
    private AudioSource audioSource;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        mat = rend.material;
        baseColor = mat.color;

        // ✅ Add AudioSource automatically
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void ActivateDestroy()
    {
        if (IsDestroyed || pulsing) return;
        StartCoroutine(DestroyRoutine());
    }

    IEnumerator DestroyRoutine()
    {
        pulsing = true;

        // 🔥 Play destroy sound once
        if (destroySound != null)
            audioSource.PlayOneShot(destroySound);

        // Pulse phase
        float t = 0f;
        while (t < pulseTime)
        {
            t += Time.deltaTime;
            mat.color = Color.Lerp(baseColor, Color.black,
                Mathf.PingPong(t * pulseSpeed, 1f));
            yield return null;
        }

        mat.color = baseColor;
        pulsing = false;
        IsDestroyed = true;

        // Shrink phase
        Vector3 originalScale = transform.localScale;
        float s = 1f;

        while (s > 0.01f)
        {
            s -= Time.deltaTime * shrinkSpeed;
            transform.localScale = originalScale * s;
            yield return null;
        }

        Destroy(gameObject);
    }
}
