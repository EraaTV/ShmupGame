using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        //Loads game
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        //Quits App
        Application.Quit();
    }
}
