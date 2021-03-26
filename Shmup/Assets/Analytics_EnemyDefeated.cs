using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Analytics_EnemyDefeated : MonoBehaviour
{
    private Scene thisScene;
    public int enemyDefeatedNum;
    public float time;

    void Awake()
    {
        thisScene = SceneManager.GetActiveScene();
    }

    public void Start()
    {
        enemyDefeatedNum = 0;
    }

    public void Update()
    {
        time += Time.deltaTime;

        //Analytics event, tracks the death of enemies at a certain amount of time
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("seconds_played", time);
        customParams.Add("EnemyDeaths", enemyDefeatedNum);
    }
}
