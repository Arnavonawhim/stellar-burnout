using UnityEngine;
using TMPro;
using System.Collections;

public class Hazard : MonoBehaviour
{
    public enum Mode { Light, Dark }
    public Mode requiredMode;            // Light hazard or Dark hazard
    public TMP_Text warningText;         // Assign in Inspector
    Coroutine timerRoutine;
    bool active = false;

    void Start()
    {
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;
        if (IsWrongMode(other) && timerRoutine == null)
            timerRoutine = StartCoroutine(Timer(other.gameObject));
    }

    void OnTriggerStay(Collider other)
    {
        if (!IsPlayer(other)) return;
        
        if (!IsWrongMode(other))
        {
            Cancel();
            return;
        }
        
        if (timerRoutine == null)
            timerRoutine = StartCoroutine(Timer(other.gameObject));
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
            Cancel();
    }

    IEnumerator Timer(GameObject player)
    {
        active = true;
        warningText.gameObject.SetActive(true);
        float t = 1f;
        
        while (t > 0f)
        {
            // still wrong?
            if (!IsWrongMode(player.GetComponent<Collider>()))
            {
                Cancel();
                yield break;
            }
            
            warningText.text = $"Player in wrong Mode ({t:0.0})";
            t -= Time.deltaTime;
            yield return null;
        }
        
        warningText.gameObject.SetActive(false);
        // respawn at checkpoint
        RespawnManager.instance.Respawn(player);
        active = false;
        timerRoutine = null;
    }

    void Cancel()
    {
        if (timerRoutine != null)
            StopCoroutine(timerRoutine);
        if (warningText != null)
            warningText.gameObject.SetActive(false);
        active = false;
        timerRoutine = null;
    }

    bool IsPlayer(Collider c)
    {
        return c.CompareTag("LightPlayer") || c.CompareTag("DarkPlayer");
    }

    bool IsWrongMode(Collider c)
    {
        // Light hazard kills Dark player
        if (requiredMode == Mode.Light && c.CompareTag("DarkPlayer"))
            return true;
        // Dark hazard kills Light player
        if (requiredMode == Mode.Dark && c.CompareTag("LightPlayer"))
            return true;
        return false;
    }
}