using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DarkPlayer") || other.CompareTag("LightPlayer"))
        {
            RespawnManager.instance.SetCheckpoint(transform.position);
        }
    }
}
