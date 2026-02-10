using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject lightCharacter;
    public GameObject darkCharacter;

    bool isLightActive = true;
    Keyboard kb;

    void Start()
    {
        kb = Keyboard.current;
        lightCharacter.SetActive(false);
        darkCharacter.SetActive(true);
        isLightActive = false;
    }

    void Update()
    {
        if (kb.eKey.wasPressedThisFrame)
        {
            Switch();
        }
    }

    void Switch()
    {
        Vector3 pos;

        if (isLightActive)
        {
            pos = lightCharacter.transform.position;
            lightCharacter.SetActive(false);
            darkCharacter.transform.position = pos;
            darkCharacter.SetActive(true);
        }
        else
        {
            pos = darkCharacter.transform.position;
            darkCharacter.SetActive(false);
            lightCharacter.transform.position = pos;
            lightCharacter.SetActive(true);
        }

        isLightActive = !isLightActive;
    }

    public bool IsLight() => isLightActive;
    public bool IsDark() => !isLightActive;
}
