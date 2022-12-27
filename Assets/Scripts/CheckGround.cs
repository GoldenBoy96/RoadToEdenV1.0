using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private GameData _gameData;
    private bool _spawnRedstone;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReadData();
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Check contact");
            _gameData._spawnGround = true;

            SaveData();

        }
        ReadData();
        if (collision.gameObject.CompareTag("Horn"))
        {
            //Debug.Log("Check contact");
            _gameData._spawnHorn = true;

            SaveData();

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

    private void SaveData()
    {
        string json = JsonUtility.ToJson(_gameData);
        //Debug.Log(json);
        System.IO.File.WriteAllText("data.txt", json);
    }
}
