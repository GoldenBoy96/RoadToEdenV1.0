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
    private float _RedstoneCooldown;
    private List<GameObject> RedStoneList = new List<GameObject>();

    public GameObject _Krill;
    private int _KrillCooldown;

    public GameObject FlyingStone1;
    public GameObject FlyingStone2;
    public GameObject FlyingStone3;
    public GameObject FlyingStone4;
    private int _FlyingStoneCooldown;
    private List<GameObject> FlyingStoneList = new List<GameObject>();
    

    public GameObject ground;
    public GameObject horn;

    

    [System.Obsolete]
    private void OnEnable()
    {
        _RedstoneCooldown = 0;
        _KrillCooldown = Random.RandomRange(700, 1500);
        _FlyingStoneCooldown = Random.RandomRange(500, 1000);

        RedStoneList = new List<GameObject>
        {
            Redstone1,
            Redstone2,
            Redstone3,
            Redstone4,
            Redstone5,
            Redstone6,
            Redstone7,
            Redstone8,
            Redstone9,
            Redstone10,
            Redstone11,
            Redstone12
        };

        FlyingStoneList = new List<GameObject>
        {
            FlyingStone1,
            FlyingStone2,
            FlyingStone3,
            FlyingStone4,
        };

        ReadData();
        _gameData._spawnRedstone = true;
        SaveData();

        
    }

    [System.Obsolete]
    void Update()
    {
        if (Time.timeScale > 0 )
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
            //for spawn krill and flying stone

            if (_gameData._speed > 0)
            {
                SpawnKrill();
                SpawnFlyingStone();
                SpawnRedStone();
            }
            
        }
        
    }

    [System.Obsolete]
    private void SpawnRedStone()
    {      
        if (_RedstoneCooldown <= 0)
        {
            int random = Random.Range(0, 11);
            GameObject RedStone = Instantiate(RedStoneList[random], transform.position, Quaternion.identity);
            RedStone.transform.position = new Vector3(RedStone.transform.position.x, -3.2f);
            _RedstoneCooldown = 1000;
        } 
        else
        {
            ReadData();
            _RedstoneCooldown -= 25/_gameData._speed;
            Debug.Log(_RedstoneCooldown);
        }

        
        
    }

    [System.Obsolete]
    private void SpawnFlyingStone()
    {
        if (_FlyingStoneCooldown <= 0)
        {
            int random = Random.Range(0, FlyingStoneList.Count);
            GameObject FlyingStone = Instantiate(FlyingStoneList[random], transform.position, Quaternion.identity);
            float randomX = Random.RandomRange(-10f, 30f);
            FlyingStone.transform.position = new Vector3(FlyingStone.transform.position.x + randomX, 15f);
            _FlyingStoneCooldown = Random.RandomRange(500, 1000);
        } else
        {
            _FlyingStoneCooldown--;
        }

           



        //Debug.Log("Test spawner");

    }

    [System.Obsolete]
    private void SpawnKrill()
    {
        if (_KrillCooldown <= 0)
        {
            GameObject Krill = Instantiate(_Krill, transform.position, Quaternion.identity);
            float randomX = Random.RandomRange(1f, 6f);
            float randomY = Random.RandomRange(1f, 6f);
            Krill.transform.position = new Vector3(30 + randomX, Krill.transform.position.y + randomY);
            _KrillCooldown = Random.RandomRange(700, 1500);
        } else
        {
            _KrillCooldown--;
        }
        

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
