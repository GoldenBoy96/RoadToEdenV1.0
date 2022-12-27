using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private string gameStatus;
    private List<KeyCode> keys;

    void Start()
    {
        
        Time.timeScale = 0;
        gameStatus = "Pause";
        keys = new List<KeyCode>();
        keys.Add(KeyCode.W);
        keys.Add(KeyCode.A);
        keys.Add(KeyCode.S);
        keys.Add(KeyCode.D);
        keys.Add(KeyCode.UpArrow);
        keys.Add(KeyCode.DownArrow);
        keys.Add(KeyCode.LeftArrow);
        keys.Add(KeyCode.RightArrow);
        keys.Add(KeyCode.Space);
    }

    void Update()
    {
        if (gameStatus == "Pause")
        {
            foreach (KeyCode key in keys)
            {
                if (Input.GetKey(key))
                {
                    Time.timeScale = 1;
                    gameStatus = "Playing";
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                gameStatus = "Pause";
                Time.timeScale = 0;
            }
        }
        
        
    }

    void Pause()
    {

    }

    void Resume()
    {

    }

    void Stop()
    {

    }

    void Restart()
    {

    }

}