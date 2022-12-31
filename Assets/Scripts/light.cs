using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class light : MonoBehaviour
{
    private GameData _gameData;
    private int _cooldown = 10;

    private void Start()
    {
        ReadData();
    }

    // Update is called once per frame
    void Update()
    {
        ReadData();
        if (_gameData._gameStatus == "End")
        {
            if (_cooldown > 0)
            {
                _cooldown--;
            } else
            {
                this.gameObject.SetActive(false);
            }
            
        }
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
}
