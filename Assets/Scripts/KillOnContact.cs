using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DarkPlayer") || other.CompareTag("LightPlayer"))
        {
            RespawnManager.instance.Respawn(other.gameObject);
        }
    }
}
