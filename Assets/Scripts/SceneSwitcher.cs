using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("ScratcherMenu");
    }

    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoodEnding()
    {
        SceneManager.LoadScene("ScratcherEnd_good");
    }

    public void BadEnding()
    {
        SceneManager.LoadScene("ScratcherEnd_bad");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
