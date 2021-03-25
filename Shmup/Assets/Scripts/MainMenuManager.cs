using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Analytics.CustomEvent("TimePlayed", new Dictionary<string, object> {
            {
                "Seconds",Time.time
            }
        }); 
        Application.Quit();
    }
}
