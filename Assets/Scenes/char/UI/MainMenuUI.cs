using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(StartWithDelay());
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(0.2f);   // lets sound play
        SceneManager.LoadScene("IntroVideo");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
