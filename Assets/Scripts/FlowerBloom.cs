using UnityEngine;
using System.Collections;

public class FlowerBloom : MonoBehaviour
{
    public GameObject nonBloomedModel;
    public GameObject bloomedModel;
    public Collider launchTrigger;

    public float upSpeed = 3f;
    public float downSpeed = 3f;

    public float firstPeak = 3f;
    public float secondPeak = 5f;

    


    public bool IsBloomed { get; private set; }
    bool isBlooming;

    Light[] lights;

    void Start()
    {
        bloomedModel.SetActive(false);
        launchTrigger.enabled = false;
        IsBloomed = false;

        lights = nonBloomedModel.GetComponentsInChildren<Light>();
        foreach (var l in lights)
            l.intensity = 0f;
    }

    public void ActivateBloom()
    {
        if (!IsBloomed && !isBlooming)
            StartCoroutine(DoBloom());
    }

    IEnumerator DoBloom()
    {
        isBlooming = true;

        yield return StartCoroutine(LerpIntensity(0f, firstPeak, upSpeed));
        yield return StartCoroutine(LerpIntensity(firstPeak, 0f, downSpeed));
        yield return StartCoroutine(LerpIntensity(0f, secondPeak, upSpeed));
        yield return StartCoroutine(LerpIntensity(secondPeak, 0f, downSpeed));

        foreach (var l in lights)
            l.intensity = 0f;

        nonBloomedModel.SetActive(false);
        bloomedModel.SetActive(true);
        launchTrigger.enabled = true;

        IsBloomed = true;
        isBlooming = false;
    }

    IEnumerator LerpIntensity(float from, float to, float speed)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            float value = Mathf.Lerp(from, to, t);
            foreach (var l in lights)
                l.intensity = value;
            yield return null;
        }
    }
}
