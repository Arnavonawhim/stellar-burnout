using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    Vector3 lastCheckpoint;
    bool hasCheckpoint = false;

    void Awake()
    {
        instance = this;
    }

    public void SetCheckpoint(Vector3 pos)
    {
        lastCheckpoint = pos;
        hasCheckpoint = true;
    }

    public void Respawn(GameObject player)
    {
        if (!hasCheckpoint)
            return;

        player.transform.position = lastCheckpoint;
    }
}
