using UnityEngine;
using Vuforia;

public class GroundPlaneManager : MonoBehaviour
{
    public GameObject planeIndicator;
    public GameObject characterToPlace;
    private bool isPlaced = false;

    void Start()
    {
        if (characterToPlace != null)
            characterToPlace.SetActive(false);
    }

    void Update()
    {
        if (planeIndicator != null && !isPlaced)
        {
            planeIndicator.SetActive(true);
        }

        if (!isPlaced)
        {
            bool tapped = false;

            if (UnityEngine.InputSystem.Mouse.current != null && 
                UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
            {
                tapped = true;
            }

            if (UnityEngine.InputSystem.Touchscreen.current != null && 
                UnityEngine.InputSystem.Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                tapped = true;
            }

            if (tapped)
            {
                PlaceCharacter();
            }
        }
    }

    void PlaceCharacter()
    {
        if (characterToPlace != null)
        {
            characterToPlace.SetActive(true);
            isPlaced = true;
            
            if (planeIndicator != null)
                planeIndicator.SetActive(false);
            
            Debug.Log("Character placed!");
        }
    }
}