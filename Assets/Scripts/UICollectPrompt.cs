using UnityEngine;
using TMPro;

public class UICollectPrompt : MonoBehaviour
{
    public static UICollectPrompt instance;
    public TextMeshProUGUI prompt;

    void Awake()
    {
        instance = this;
        prompt.gameObject.SetActive(false);
    }

    public void ShowPrompt(string msg)
    {
        prompt.text = msg;
        prompt.gameObject.SetActive(true);

        CancelInvoke(nameof(HidePrompt));
        Invoke(nameof(HidePrompt), 2f);
    }

    void HidePrompt() => prompt.gameObject.SetActive(false);
}
