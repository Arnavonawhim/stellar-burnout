using UnityEngine;


public class DissolveSphere : MonoBehaviour
{
    Renderer[] renderers;

    void Start()
    {
        // Gets ALL mesh + skinned mesh renderers
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        float dissolve = Mathf.PingPong(Time.time, 1f);

        foreach (Renderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                if (m.HasProperty("_DissolveAmount"))
                {
                    m.SetFloat("_DissolveAmount", dissolve);
                }
            }
        }
    }
}