using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using static FileHandler.FileHandler;
using Assets.Scripts;
using System.IO;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _highestText;

    //public UnityEvent OnTextChange; //use for animation

    private int _score;
    private int _highestScore;
    private GameData _gameData;
    private float _speed;
    private int _cooldown;

    private void Awake()
    {
        ReadData();
        _speed = 5;
        _gameData._speed = _speed;
        SaveData();
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        SetScore(_score);
        ReadData();
        _cooldown = 100;
        _highestScore = _gameData._highestScore;
        SetHighestScore(_highestScore);
        Time.timeScale = 0;

    }

    // Update is called once per frame

    void Update()
    {
        if (Time.timeScale > 0)
        {
            ReadData();
            _speed = _gameData._speed;


            if (_gameData._speed > 0)
            {
                _score += _gameData._level switch
                {
                    "Easy" => 1,
                    "Medium" => 2,
                    "Hard" => 3,
                    _ => 1,
                };
                _gameData._score = _score;
                SetScore(_score);

                if (_cooldown > 0)
                {
                    _cooldown--;
                }
                else
                {
                    _gameData._speed += _gameData._level switch
                    {
                        "Easy" => 0.01f,
                        "Medium" => 0.03f,
                        "Hard" => 0.05f,
                        _ => 0.01f,
                    };
                    //Debug.Log(_gameData._speed);
                    SaveData();
                    _cooldown = 100;
                }
            }



            if (_score >= _highestScore)
            {
                _highestScore = _score;
            }

            _gameData._highestScore = _highestScore;




            SaveData();
        }


    }

    private void SetScore(int val)
    {
        _scoreText.SetText("Score: " + val.ToString().PadLeft(8, '0'));

    }

    private void SetHighestScore(int val)
    {
        _highestText.SetText("Highest: " + val.ToString().PadLeft(8, '0'));

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
