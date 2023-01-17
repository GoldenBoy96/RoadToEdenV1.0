using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkykidController : MonoBehaviour
{
    private Rigidbody2D _rb2D;

    private float _jumpForce;
    private float _speed;
    private float _playerSpeed;
    private bool _isContactWallRight;
    private bool _isContactWallLeft;
    private bool _isGround;
    private GameData _gameData;
    private int _jumpCooldown;


    // Start is called before the first frame update
    void Start()
    {
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        _playerSpeed = 5.5f;
        _speed = 5f;
        _jumpForce = 15f;
        _isContactWallRight = false;
        _isContactWallLeft = false;
        _isGround = false;

        ReadData();
        _gameData._energy = 500;
        SaveData();

        _jumpCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_jumpCooldown > 0)
        {
            _jumpCooldown--;
        }
        ReadData();
        _speed = _gameData._speed;
        if (_speed > 0)
        {
            //Dùng cho việc sạc năng lượng
            //Khi bay năng lượng sẽ sạc rất chậm, ngược lại khi chạm đất sẽ sạc nhanh hơn nhiều lần
            EnergyRecharge();
            //Debug.Log("Energy: " + _gameData._energy);

            //Horizontal ở đây mặc định sẽ là 2 mũi tên trái phải cùng kí tự A D
            //moveHorizontal = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            //moveVertical = Input.GetAxisRaw("Vertical");
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || _gameData._swipeControl.Equals("SwipeRight"))
            {
                MoveRight();
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || _gameData._swipeControl.Equals("SwipeLeft"))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || _gameData._swipeControl.Equals("SwipeUp"))
            {
                if (_jumpCooldown == 0)
                {
                    Fly();
                    _jumpCooldown = 10;
                }
               

            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || _gameData._swipeControl.Equals("SwipeDown"))
            {
                Plunge();
            }
            ReadData();
            _speed = _gameData._speed;
        }



    }

    void EnergyConsume()
    {
        ReadData();
        if (_gameData._energy >= 0)
        {
            _gameData._energy -= 100;
        }
        SaveData();
    }

    void EnergyRecharge()
    {
        if (Time.timeScale > 0)
        {
            ReadData();
            if (_gameData._energy < 0)
            {
                _gameData._energy = 0;
            }
            else if (_gameData._energy > 500)
            {
                _gameData._energy = 500;
            }
            else
            {
                if (_isGround)
                {
                    _gameData._energy += _gameData._speed / 2;
                }
                else
                {
                    _gameData._energy += _gameData._speed / 15;
                }
            }


            SaveData();
        }

    }

    public void MoveRight()
    {
        if (!_isContactWallRight)
        {
            if (_isGround)
            {
                _rb2D.velocity = new Vector2(_playerSpeed - 2, _rb2D.velocity.y);
            }
            else
            {
                _rb2D.velocity = new Vector2((_playerSpeed - 2) / 2, _rb2D.velocity.y);
            }

        }
    }

    public void MoveLeft()
    {
        if (!_isContactWallLeft)

        {
            if (_isGround)
            {
                _rb2D.velocity = new Vector2(-_playerSpeed, _rb2D.velocity.y);
            }
            else
            {
                _rb2D.velocity = new Vector2(-_playerSpeed / 2, _rb2D.velocity.y);
            }
        }
    }

    public void Fly()
    {
        if (_gameData._energy >= 100)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, 7f);
            _rb2D.AddForce(transform.up * _jumpForce);
            EnergyConsume();
        }
        else if (_gameData._energy >= 50 && _gameData._energy < 100)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, 3f);
            _rb2D.AddForce(transform.up * _jumpForce);
            EnergyConsume();
        }
    }

    public void Plunge()
    {
        if (!_isGround)
        {
            if (_rb2D.velocity.y > -5)
            {
                _rb2D.velocity += new Vector2(0, -3f);
            }

            if (_rb2D.velocity.y > -15 && _rb2D.velocity.y <= -5)
            {
                _rb2D.velocity += new Vector2(0, -0.5f);
            }

            //Debug.Log(_rb2D.velocity);
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
        if (collision.gameObject.CompareTag("Obstancle") || collision.gameObject.CompareTag("Krill") || collision.gameObject.CompareTag("FlyingStone"))
        {
            _speed = 0;
            _gameData._speed = _speed;
            _gameData._gameStatus = "End";
            SaveData();

        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGround = true;
            //Debug.Log(_isGround);
        }
        if (collision.gameObject.CompareTag("WallRight"))
        {
            _isContactWallRight = true;
            //Debug.Log(_isContactWall);
        }
        if (collision.gameObject.CompareTag("WallLeft"))
        {
            _isContactWallLeft = true;
            //Debug.Log(_isContactWall);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WallRight"))
        {
            _isContactWallRight = false;
            //Debug.Log(_isContactWall);
        }
        if (collision.gameObject.CompareTag("WallLeft"))
        {
            _isContactWallLeft = false;
            //Debug.Log(_isContactWall);
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGround = false;
            //Debug.Log(_isGround);
        }
    }


}




