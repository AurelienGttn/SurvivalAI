using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameOver;
    private bool started;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameUI;

    void Start()
    {
        gameOver = false;
        started  = false;
    }

    private void Update()
    {
        if (!started)
        {
            Time.timeScale = 0;
            mainMenu.SetActive(true);
            gameUI.SetActive(false);
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
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

    public void Pause()
    {
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

    public void StartGame()
    {
        SceneManager.LoadScene("BaseMap");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
