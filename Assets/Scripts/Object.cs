using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Object : MonoBehaviour
{
    private float leftEdge;
    private float _speed;
    private GameData _gameData;

    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 30f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.layer = 2;

        ReadData();
        _speed = _gameData._speed;

        if (_speed > 0)
        {
            MoveLeft();
        }
    }

    public void MoveLeft()
    {
        Vector3 tmp = transform.position;

        tmp.x -= _speed * Time.deltaTime;
        transform.position = tmp;

        if (transform.position.x < leftEdge)
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
