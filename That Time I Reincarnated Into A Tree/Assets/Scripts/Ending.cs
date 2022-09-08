using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public Animator crossFadeAnimator;
    public float transitionTime = 2f;

    public void RestartGame()
    {
        StartCoroutine(LoadRestart());
    }

    IEnumerator LoadRestart()
    {
        crossFadeAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}