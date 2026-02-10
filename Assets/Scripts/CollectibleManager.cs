using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance;

    public int totalLight { get; private set; }
    public int totalDark { get; private set; }

    public TextMeshProUGUI uiText;

    void Awake() => instance = this;

    public void CollectLight()
    {
        totalLight++;
        UpdateUI();
    }

    public void CollectDark()
    {
        totalDark++;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (uiText)
            uiText.text = $"Light: {totalLight}   Dark: {totalDark}";
    }
}
