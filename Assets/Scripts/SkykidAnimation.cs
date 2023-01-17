using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkykidAnimation : MonoBehaviour
{
    private Animator _anim;
    private bool _isGround;

    public AudioSource _AudioDead;
    public AudioSource _AudioFly;
    public AudioSource _AudioLand;

    private GameData _gameData;

    private int _jumpCooldown;
    private bool _isPlunge = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _jumpCooldown = 0;
        _isPlunge = false;
    }

    // Update is called once per frame
    void Update()
    {
        Flyfa(Get_AudioFly());


    }


    public void Move(float Move)
    {
        _anim.SetFloat("Move", Mathf.Abs(Move));
    }

    public void Jump(int jumping)
    {

            _anim.SetInteger("Jumping", jumping);

           
        
    }

    public AudioSource Get_AudioFly()
    {
        return _AudioFly;
    }

    public void Flyfa(AudioSource _AudioFly)
    {
        if (_jumpCooldown > 0)
        {
            _jumpCooldown--;
        }
        ReadData();
        if (_isGround)
        {
            Jump(-1);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || _gameData._swipeControl.Equals("SwipeUp"))
            {
                
                if (_gameData._energy > 100 && _jumpCooldown == 0)
                {
                    Jump(1);
                    _AudioFly.Play();
                    _jumpCooldown = 10;
                }
            }
        } else { 
            Jump(0);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || _gameData._swipeControl.Equals("SwipeUp"))
            {
                
                if (_gameData._energy > 50 && _jumpCooldown == 0)
                {
                    Jump(1);
                    _AudioFly.Play();
                    _jumpCooldown = 20;
                }
                    
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                Jump(2);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                Jump(4);
            }
            if (_gameData._swipeControl.Equals("SwipeDown"))
            {
                Jump(2);
                _isPlunge = true;
            }
            if (_isPlunge && _gameData._swipeControl.Equals("None"))
            {
                Jump(4);
                _isPlunge = false;
            }
        }
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstancle") || collision.gameObject.CompareTag("Krill") || collision.gameObject.CompareTag("FlyingStone"))
        {
            _anim.SetInteger("Dead", 1);
            _AudioDead.Play();
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGround = true;
            if (Time.timeScale>0)
            {
                _AudioLand.Play();
            }
            
            //Debug.Log(_isGround);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGround = false;

            //Debug.Log(_isGround);
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
