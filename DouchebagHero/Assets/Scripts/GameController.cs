using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameOver;
    private bool started;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameUI;

    void Start()
    {
        gameOver = false;
        started  = false;
        mainMenu.SetActive(true);
    }

    private void Update()
    {
        if (!started)
        {
            Time.timeScale = 0;
            gameUI.SetActive(false);
        }

        if (gameOver)
        {
            StartCoroutine(GameOver());
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        started = true;
        Time.timeScale = 1;
    }

    public void ShowSettings()
    {
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            if (!started)
            {
                mainMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }
        else
        {
            if (!started)
            {
                mainMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
            settingsMenu.SetActive(true);
        }
    }

    public void Pause()
    {
        if (!started)
            return;
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("BaseMap");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);

        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }
}
