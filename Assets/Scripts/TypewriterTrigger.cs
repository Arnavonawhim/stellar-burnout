using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterTrigger : MonoBehaviour
{
    [TextArea] public string message;
    public TMP_Text uiText;
    public float typingSpeed = 0.04f;
    public float displayTime = 3f; // time after typing to keep text visible

    bool triggered = false;

    void Start()
    {
        if (uiText != null)
            uiText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player") || other.CompareTag("LightPlayer") || other.CompareTag("DarkPlayer"))
        {
            triggered = true;
            StartCoroutine(TypeMessage());
        }
    }

    IEnumerator TypeMessage()
    {
        uiText.text = "";
        uiText.gameObject.SetActive(true);

        // typewriter
        foreach (char c in message)
        {
            uiText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // wait display time
        yield return new WaitForSeconds(displayTime);

        // hide text
        uiText.gameObject.SetActive(false);
    }
}
