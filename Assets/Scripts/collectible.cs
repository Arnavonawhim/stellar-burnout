using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum Type { Light, Dark }
    public Type type;

    void OnTriggerEnter(Collider other)
    {
        if (type == Type.Light && other.CompareTag("LightPlayer"))
        {
            CollectibleManager.instance.CollectLight();
            Destroy(gameObject);
        }
        else if (type == Type.Dark && other.CompareTag("DarkPlayer"))
        {
            CollectibleManager.instance.CollectDark();
            Destroy(gameObject);
        }
    }
}
