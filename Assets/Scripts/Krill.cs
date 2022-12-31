using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Krill : MonoBehaviour
{
    private Animator _anim;
    private float leftEdge;
    private float _speed;
    private GameData _gameData;
    private bool _isTrigger;
    private Vector3 _targetPosition;
    private int _angle;

    public AudioSource _AudioKrillAppear;
    public AudioSource _AudioKrillAttack;

    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 50f;
        _isTrigger = false;
        _angle = 0;
        _anim = GetComponent<Animator>();
        _AudioKrillAppear.Play();
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

            if (_isTrigger)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 20 * Time.deltaTime);
                if (_angle < 30)
                {
                    RotateLeft(_angle);
                    _angle++;
                }
            }
        }

        
    }

    public void MoveLeft()
    {
        Vector3 tmp = transform.position;

        tmp.x -= _speed * 1.3f * Time.deltaTime;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && !_isTrigger)
        {
            _targetPosition = collision.gameObject.transform.position;
            _targetPosition.x -= 400;
            _targetPosition.y -= 500;
            _isTrigger = true;
            _anim.SetBool("_isTrigger", true);
            _AudioKrillAppear.Pause();
            _AudioKrillAttack.Play();
        }
        
    }

    void RotateLeft(int angle)
    {
        transform.Rotate(Vector3.forward * angle / 20);
    }
}
