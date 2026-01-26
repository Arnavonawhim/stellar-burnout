using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    Vector3 checkpointPosition;

    void Awake()
    {
        instance = this;

        PlayerSpawn spawn = FindFirstObjectByType<PlayerSpawn>();
        if (spawn != null)
            checkpointPosition = spawn.transform.position;
    }

    public void SetCheckpoint(Vector3 pos)
    {
        checkpointPosition = pos;
    }

    public void Respawn(GameObject player)
    {
        player.transform.position = checkpointPosition;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb) rb.linearVelocity = Vector3.zero;
    }
}
