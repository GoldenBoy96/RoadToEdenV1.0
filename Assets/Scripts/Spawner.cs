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
    //private float _RedstoneCooldown;
    private List<GameObject> RedStoneList = new List<GameObject>();
    private float RedstoneDistance;

    public GameObject _Krill;
    private int _KrillCooldown;

    public GameObject FlyingStone1;
    public GameObject FlyingStone2;
    public GameObject FlyingStone3;
    public GameObject FlyingStone4;
    private int _FlyingStoneCooldown;
    private List<GameObject> FlyingStoneList = new List<GameObject>();

    public GameObject _WingedLight;
    private int _WingedLightCooldown;


    public GameObject ground;
    public GameObject horn;



    [System.Obsolete]
    private void OnEnable()
    {
        ReadData();
        RedstoneLevel();
        KrillLevel();
        FlyingStoneLevel();
        WingedLightLevel();

        if (_gameData._level.Equals("Nightmare"))
        {
            _KrillCooldown = 1000;
        }


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
            if (_gameData._spawnRedstone)
            {
                SpawnRedStone();
                _gameData._spawnRedstone = false;
                SaveData();
            }
            //for spawn krill and flying stone

            if (_gameData._speed > 0)
            {
                SpawnKrill();
                SpawnFlyingStone();
                SpawnWingedLight();
            }

        }

    }

    [System.Obsolete]
    private void SpawnRedStone()
    {

        int random = Random.Range(0, 11);
        GameObject RedStone = Instantiate(RedStoneList[random], transform.position, Quaternion.identity);
        RedstoneLevel();
        
        RedStone.transform.position = new Vector3(RedStone.transform.position.x + RedstoneDistance, -3.2f);

    }

    [System.Obsolete]
    private void RedstoneLevel()
    {
        switch (_gameData._level)
        {
            case "Easy":
               RedstoneDistance = Random.RandomRange(0, 25);
                break;
            case "Medium":
                RedstoneDistance = Random.RandomRange(0, 20);
                break;
            case "Hard":
                RedstoneDistance = Random.RandomRange(0, 15);
                break;
            default:
                RedstoneDistance = Random.RandomRange(0, 20);
                break;
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

            FlyingStoneLevel();


        }
        else
        {
            _FlyingStoneCooldown--;
        }

    }

    [System.Obsolete]
    private void FlyingStoneLevel()
    {
        switch (_gameData._level)
        {
            case "Easy":
                _FlyingStoneCooldown = Random.RandomRange(500, 1000);
                break;
            case "Medium":
                _FlyingStoneCooldown = Random.RandomRange(400, 850);
                break;
            case "Hard":
                _FlyingStoneCooldown = Random.RandomRange(300, 700);
                break;
            default:
                _FlyingStoneCooldown = Random.RandomRange(500, 1000);
                break;
        }
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
            KrillLevel();
            
        }
        else
        {
            _KrillCooldown--;
        }

    }

    [System.Obsolete]
    private void KrillLevel()
    {
        _KrillCooldown = _gameData._level switch
        {
            "Easy" => Random.RandomRange(700, 1500),
            "Medium" => Random.RandomRange(600, 1200),
            "Hard" => Random.RandomRange(500, 900),
            "Nightmare" => Random.RandomRange(10, 30),
            _ => Random.RandomRange(700, 1500),
        };
    }

    [System.Obsolete]
    private void SpawnWingedLight()
    {
        if (_WingedLightCooldown <= 0)
        {
            GameObject wingedLight = Instantiate(_WingedLight, transform.position, Quaternion.identity);
            wingedLight.transform.position = new Vector3(30, -3.2f);
            WingedLightLevel();

        }
        else
        {
            _WingedLightCooldown--;
        }

    }

    [System.Obsolete]
    private void WingedLightLevel()
    {
        _WingedLightCooldown = _gameData._level switch
        {
            "Easy" => Random.RandomRange(500, 2000),
            "Medium" => Random.RandomRange(500, 3000),
            "Hard" => Random.RandomRange(500, 4000),
            _ => Random.RandomRange(500, 2000),
        };
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
