using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class Menu : MonoBehaviour
{
    private GameData _gameData;


    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 0;

    }

    public void Tutorial()
    {
        EnterGame("Tutorial");
    }

    public void Easy()
    {
        EnterGame("Easy");
    }

    public void Medium()
    {
        EnterGame("Medium");
    }

    public void Hard()
    {
        EnterGame("Hard");
    }

    public void Nightmare()
    {
        EnterGame("Nightmare");
    }

    public void Exit()
    {
        //throw new NotImplementedException();
    }

    private void EnterGame(string level)
    {
        ReadData();
        _gameData._level = level;
        SaveData();
        SceneManager.LoadScene("Loading");
        
    }


    private void ReadData()
    {
        string jsonRead;
        try
        {
            jsonRead = System.IO.File.ReadAllText("data.txt");
            _gameData = JsonUtility.FromJson<GameData>(jsonRead);
        }
        catch (FileNotFoundException)
        {
            _gameData = new GameData();
            string json = JsonUtility.ToJson(_gameData);
            System.IO.File.WriteAllText("data.txt", json);
        }
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(_gameData);
        //Debug.Log(json);
        System.IO.File.WriteAllText("data.txt", json);
    }
}
