using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

  void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("ESC Pressed!");

        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }
}

    // PAUSE GAME
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // RESUME GAME
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // RESTART GAME
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Palakkascene");
    }

    // EXIT TO MAIN MENU
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("uiscene");
    }
}
