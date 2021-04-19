using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        unlocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (unlocked == true)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
