using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkykidAnimation : MonoBehaviour
{
    private Animator _anim;
    private bool _isGround;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Move(float Move)
    {
        _anim.SetFloat("Move", Mathf.Abs(Move));
    }

    public void Jump(int jumping)
    {
        _anim.SetInteger("Jumping", jumping);
    }

    public void Flyfa()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        //{
        //    Jump(1);
        //} 
        if (_isGround)
        {
            Jump(-1);
        } else { 
            Jump(0);
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump(1);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                Jump(2);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Flyfa();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstancle"))
        {
            _anim.SetInteger("Dead", 1);

        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            _isGround = true;
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

}
