using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeFirstScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("FirstScene");
    }
    public void ChangeMainScene()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("MainScene");
    }

    public void ChangeThirdScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ThirdScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
