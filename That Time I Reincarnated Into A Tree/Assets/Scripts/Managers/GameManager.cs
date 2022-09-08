using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverCanvas;
    public Animator crossFadeAnim;

    public float transitionTime = 2f;

    public string nextSceneName;
    public string alternateScene;
    public int killThreshold;

    public bool gameOver;

    public int enemiesKilled = 0;

    private void Awake()
    {
        Instance = this;

        gameOverCanvas.SetActive(false);
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        PlayerShoot.canFire = false;
        Debug.Log("gameover");
        ActivateGameOverUI();
    }

    public void Victory()
    {
        StartCoroutine(LoadNextScene());
    }

    void ActivateGameOverUI()
    {
        gameOverCanvas.SetActive(true);
    }
    
    public void ReloadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        PlayerShoot.canFire = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    IEnumerator LoadNextScene()
    {
        if (enemiesKilled < killThreshold)
        {
            crossFadeAnim.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            crossFadeAnim.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(alternateScene);
        }
    }
}

public enum GameState
{
    Running,
    Victory,
    GameOver
}