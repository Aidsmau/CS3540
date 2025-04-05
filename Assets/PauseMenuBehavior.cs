using UnityEngine;

public class PauseMenuBehavior : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
