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
        lightCharacter.SetActive(true);
        darkCharacter.SetActive(false);
    }

    void Update()
    {
        if (kb.eKey.wasPressedThisFrame)
        {
            isLightActive = !isLightActive;
            lightCharacter.SetActive(isLightActive);
            darkCharacter.SetActive(!isLightActive);
        }
    }

    public bool IsLight() => isLightActive;
    public bool IsDark() => !isLightActive;
}
