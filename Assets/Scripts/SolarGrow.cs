using UnityEngine;
using System.Collections;

public class SolarGrow : MonoBehaviour
{
    public float growSpeed = 1.5f;
    public float maxHeight = 5f;

    public Color pulseColor = Color.yellow;
    public float pulseSpeed = 4f;

    bool isGrowing = false;
    public bool IsMaxed { get; private set; }

    Renderer rend;
    Material mat;
    Color baseColor;
    Vector3 startScale;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        mat = rend.material;
        baseColor = mat.color;

        startScale = transform.localScale;
    }

    public void StartGrowth()
    {
        if (IsMaxed || isGrowing) return;
        StartCoroutine(GrowRoutine());
    }

    IEnumerator GrowRoutine()
    {
        isGrowing = true;

        float t = 0f;
        while (transform.localScale.y < maxHeight)
        {
            // growth
            float newY = transform.localScale.y + growSpeed * Time.deltaTime;
            if (newY >= maxHeight)
            {
                newY = maxHeight;
                IsMaxed = true;
            }

            transform.localScale = new Vector3(startScale.x, newY, startScale.z);

            // pulse
            t += Time.deltaTime * pulseSpeed;
            Color c = Color.Lerp(baseColor, pulseColor, Mathf.PingPong(t, 1f));
            mat.color = c;

            yield return null;
        }

        // reset color to normal
        mat.color = baseColor;

        isGrowing = false;
    }
}
