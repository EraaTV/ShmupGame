using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hi : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        print("Hello world!");
    }

    private void Update()
    {
        if (Time.time > 5)
        {
            print("It has been 5 seconds!");
        }
    }
}
