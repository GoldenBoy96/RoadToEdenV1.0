using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.IO;
using UnityEngine.Playables;

public class RedStone : MonoBehaviour, IObstacle
{

    private float leftEdge;
    private Animator animator;
    private float _speed;
    private GameData _gameData;



    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 50f;

        //animator = GetComponent<Animator>();
        //int random = Random.Range(1, 12);
        //animator.Play("Restone" + random.ToString());
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
            _gameData._speed += 0.000005f;
            //Debug.Log(_gameData._speed);
            SaveData();
        }

        
    }

    public void BurnPlayer()
    {
        //Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            BurnPlayer();
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
