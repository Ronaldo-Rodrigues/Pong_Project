using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Paddle : Paddle
{
    private Vector2 _direction;

    public bool isFirePaddle = false;
    public GameObject firePaddle;

    public bool isAquaPaddle = false;
    public GameObject aquaPaddle;

    public bool isGrassPaddle = false;
    public GameObject grassPaddle;


    private void Start()
    {
        firePaddle.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else
        {
            _direction = Vector2.zero;
        }

        //tentativa de ativar as magias
        if (Input.GetKey(KeyCode.Q))
        {
            isFirePaddle = true;
            firePaddle.SetActive(true); 
        }
    }

    private void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0)
        {
            _rb.AddForce(_direction * speed);
        }    
    }
}
