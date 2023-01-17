using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Honk : MonoBehaviour
{
    private Animator _anim;

    private AudioSource _honk;
    public AudioSource _honk1;
    public AudioSource _honk2;
    public AudioSource _honk3;
    public AudioSource _honk4;
    public AudioSource _honk5;
    List<AudioSource> AudioList;

    private int _cooldown;

    private GameData _gameData;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("honk", false);
        _cooldown = 0;

        AudioList = new List<AudioSource>
        {
            _honk1,
            _honk2,
            _honk3,
            _honk4,
            _honk5,
        };

    }
    // Update is called once per frame
    void Update()
    {
        ReadData();
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) || _gameData._touchHonk)
            {
                _anim.SetBool("honk", false);
                _anim.SetBool("honk", true);
                int random = Random.Range(0, AudioList.Count);
                _honk = AudioList[random];
                _honk.Play();
                _cooldown = 30;
            }
            if (_cooldown > 0)
            {
                _cooldown--;
            }
            else
            {
                _anim.SetBool("honk", false);

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

    private void SaveData()
    {
        string json = JsonUtility.ToJson(_gameData);
        //Debug.Log(json);
        System.IO.File.WriteAllText("data.txt", json);
    }
}
