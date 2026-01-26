using UnityEngine;

public class Saw : MonoBehaviour
{
    public float rotateSpeed = 720f;

    void Update()
    {
        transform.Rotate(0f,rotateSpeed * Time.deltaTime, 0f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DarkPlayer") || other.CompareTag("LightPlayer"))
        {
            RespawnManager.instance.Respawn(other.gameObject);
        }
    }
}
