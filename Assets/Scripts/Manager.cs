using Assets.Scripts;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private GameData _gameData;
    private string gameStatus;
    private List<KeyCode> keys;
    public GameObject _restartButton;
    public GameObject _pauseButton;
    private bool _start = false;
    private bool _restart = false;
    private bool _pause = false;
    private int _cooldown = 60;
    GameObject pauseButton;

    public AudioSource _AudioBGMusic;
    public AudioSource _AudioBGMusic1;
    public AudioSource _AudioBGMusic2;
    public AudioSource _AudioBGMusic3;
    public AudioSource _AudioBGMusic4;
    public AudioSource _AudioBGMusic5;
    public AudioSource _AudioBGMusic6;
    public AudioSource _AudioBGMusic7;
    List<AudioSource> AudioList;

    [System.Obsolete]
    void Start()
    {

        Time.timeScale = 0;
        gameStatus = "Pause";
        ReadData();
        _gameData._gameStatus = gameStatus;
        SaveData();
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

        AudioList = new List<AudioSource>
        {
            _AudioBGMusic1,
            _AudioBGMusic2,
            _AudioBGMusic3,
            _AudioBGMusic4,
            _AudioBGMusic5,
            _AudioBGMusic6,
            _AudioBGMusic7,
        };
        int random = Random.RandomRange(0, AudioList.Count); 
        _AudioBGMusic = AudioList[random];

    }

    void Update()
    {
        ReadData();
        gameStatus = _gameData._gameStatus;

        if (!_start)
        {
            foreach (KeyCode key in keys)
            {
                if (Input.GetKey(key) || Input.GetMouseButtonDown(0))
                {
                    Time.timeScale = 1;
                    _gameData._gameStatus = "Playing";
                    SaveData();
                    _pause = false;
                    _start = true;
                    _AudioBGMusic.Play();
                }
            }
        }
        else
        {
            if (gameStatus == "Pause" && !_pause)
            {
                _pause = true;
                pauseButton = Instantiate(_pauseButton, Vector3.zero, Quaternion.identity);
            }
            else if (gameStatus == "Pause" && _pause)
            {
                foreach (KeyCode key in keys)
                {
                    if (Input.GetKey(key))
                    {
                        Time.timeScale = 1;
                        _gameData._gameStatus = "Playing";
                        SaveData();
                        _pause = false;
                        Destroy(pauseButton);
                        _AudioBGMusic.UnPause();
                    }
                }
            }
            else if (gameStatus == "End" && !_restart)
            {

                if (_cooldown > 0)
                {
                    _cooldown--;
                }
                else
                {
                    GameObject restartButton = Instantiate(_restartButton, Vector3.zero, Quaternion.identity);
                    _restart = true;
                }

            }
            else if (gameStatus == "End" && _restart)
            {
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetMouseButtonDown(0))
                {
                    _gameData._gameStatus = "Pause";
                    SaveData();
                    SceneManager.LoadScene("Main");
                    Time.timeScale = 0;
                }
            }
            else if (gameStatus == "Playing")
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    _gameData._gameStatus = "Pause";
                    SaveData();
                    Time.timeScale = 0;
                    _AudioBGMusic.Pause();
                }
            }

            //Debug.Log(gameStatus);
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