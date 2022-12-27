using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameData _gameData;
    private bool _spawnRedstone;

    public GameObject Redstone1;
    public GameObject Redstone2;
    public GameObject Redstone3;
    public GameObject Redstone4;
    public GameObject Redstone5;
    public GameObject Redstone6;
    public GameObject Redstone7;
    public GameObject Redstone8;
    public GameObject Redstone9;
    public GameObject Redstone10;
    public GameObject Redstone11;
    public GameObject Redstone12;

    public GameObject ground;
    public GameObject horn;

    private List<GameObject> RedStoneList = new List<GameObject>();

    private void OnEnable()
    {
        RedStoneList = new List<GameObject>();
        RedStoneList.Add(Redstone1);
        RedStoneList.Add(Redstone2);
        RedStoneList.Add(Redstone3);
        RedStoneList.Add(Redstone4);
        RedStoneList.Add(Redstone5);
        RedStoneList.Add(Redstone6);
        RedStoneList.Add(Redstone7);
        RedStoneList.Add(Redstone8);
        RedStoneList.Add(Redstone9);
        RedStoneList.Add(Redstone10);
        RedStoneList.Add(Redstone11);
        RedStoneList.Add(Redstone12);

        ReadData();
        _gameData._spawnRedstone = true;
        SaveData();
    }

    [System.Obsolete]
    void Update()
    {
        if (Time.timeScale > 0)
        {
            ReadData();
            //Debug.Log(_gameData._spawnRedstone);
            if (_gameData._spawnRedstone)
            {
                SpawnRedStone();
                _gameData._spawnRedstone = false;
                SaveData();
            }
            if (_gameData._spawnGround)
            {
                SpawnGround();
                _gameData._spawnGround = false;
                SaveData();
            }
            if (_gameData._spawnHorn)
            {
                SpawnHorn();
                _gameData._spawnHorn = false;
                SaveData();
            }
        }
        
    }

    [System.Obsolete]
    private void SpawnRedStone()
    {      
        

        int random = Random.Range(0, 11);
        GameObject RedStone = Instantiate(RedStoneList[random], transform.position, Quaternion.identity);
        float randomX = Random.RandomRange(-3f, 3f);
        RedStone.transform.position = new Vector3(RedStone.transform.position.x + randomX, -3.2f);
        
   

        //Debug.Log("Test spawner");

    }

    private void SpawnGround()
    {
        GameObject Ground = Instantiate(ground, transform.position, Quaternion.identity);
        Ground.transform.position = new Vector3(20f, -4.2f);
    }

    private void SpawnHorn()
    {
        GameObject Horn = Instantiate(horn, transform.position, Quaternion.identity);
        Horn.transform.position = new Vector3(20f, -1.84f);
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
