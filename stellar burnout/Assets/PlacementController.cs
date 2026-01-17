using UnityEngine;
using Vuforia;
using UnityEngine.InputSystem;

public class PlacementController : MonoBehaviour
{
    public GameObject contentToPlace; 
    private bool isPlaced = false;
    
    void Update()
    {
        if (!isPlaced)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                PlaceContent();
            }
            
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                PlaceContent();
            }
        }
    }
    
    void PlaceContent()
    {
        if (contentToPlace != null)
        {
            contentToPlace.SetActive(true);
            isPlaced = true;
            Debug.Log("Content placed!");
        }
    }
}