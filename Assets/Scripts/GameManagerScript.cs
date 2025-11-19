using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public float gameTime;
    public bool gameActive;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
            Instance = this;
    }

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if (gameActive)
        {
            gameTime += Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                Pause();
            }
        }
    }

    public void GameOver()
    {
        gameActive = false;
        AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.gameOver);
        StartCoroutine(ShowGameOverScreen());
    }

    public void SaveScore(string playerName)
    {
        if (LeaderboardScript.Instance != null)
        {
            LeaderboardScript.Instance.AddScore(playerName, gameTime);
        }
    }
    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
        UIController.Instance.gameOverPanel.SetActive(true);
        UIController.Instance.playerNameInput.gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        if(UIController.Instance.pausePanel.activeSelf == 
            false && UIController.Instance.gameOverPanel.activeSelf==false )
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.pause);
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            AudioControllerScript.Instance.PlaySound(AudioControllerScript.Instance.resume);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); 
        Time.timeScale = 1f;
    }
}
