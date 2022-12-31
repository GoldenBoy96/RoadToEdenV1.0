using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlyingStone : MonoBehaviour
{
    private Vector3 _start;
    private Vector3 _end;
    private GameData _gameData;
    private float _angle;
    public AudioSource _AudioFlyingStone;
    private bool _soundPlaying;

    // Start is called before the first frame update
    void Start()
    {
        _start = transform.position;
        int randomX = Random.Range(-30, -10);
        _end = new Vector3(randomX, -10);

        //this.transform.position = _end;
        _angle = Mathf.Atan((_start.y - _end.y) / (_start.x - _end.x)) / (Mathf.PI / 180) * 0.6f;
        this.transform.Rotate(0, 0, _angle);
        //Debug.Log(_start);
        //Debug.Log(_end);
        //Debug.Log(_angle);
        _soundPlaying = false;

    }

    // Update is called once per frame
    void Update()
    {
        ReadData();
        if (_gameData._speed > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _end, 10 * Time.deltaTime);

            if (!_soundPlaying)
            {
                if (transform.position.y < 7)
                {
                    _soundPlaying = true;
                    _AudioFlyingStone.Play();
                }
            }

        }
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
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
