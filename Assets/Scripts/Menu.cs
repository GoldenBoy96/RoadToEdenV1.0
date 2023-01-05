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

    //public Button _Tutorial;
    //public Button _Easy;
    //public Button _Medium;
    //public Button _Hard;
    //public Button _Nightmare;
    //public Button _Exit;

    // Start is called before the first frame update
    void Start()
    {
        //Button TutorialButton = _Easy.GetComponent<Button>();
        //Button EasyButton = _Easy.GetComponent<Button>();
        //Button MediumButton = _Easy.GetComponent<Button>();
        //Button HardButton = _Easy.GetComponent<Button>();
        //Button NightmareButton = _Easy.GetComponent<Button>();
        //Button ExitButton = _Easy.GetComponent<Button>();

        //TutorialButton.onClick.AddListener(Tutorial);
        //EasyButton.onClick.AddListener(Easy);
        //MediumButton.onClick.AddListener(Medium);
        //HardButton.onClick.AddListener(Hard);
        //NightmareButton.onClick.AddListener(Nightmare);
        //ExitButton.onClick.AddListener(Exit);

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
        SceneManager.LoadScene("Main");
        
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
