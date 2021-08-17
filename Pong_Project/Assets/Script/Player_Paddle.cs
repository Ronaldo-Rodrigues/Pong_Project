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

    public bool canCastMagic = true;


    private void Start()
    {
        firePaddle.SetActive(false);
        aquaPaddle.SetActive(false);
        grassPaddle.SetActive(false);
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

        // ativar as magias
        if(canCastMagic == true)
        {
            //Fire
            if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
            {
                FirePaddleOn();
            }
            //Aqua
            if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            {
                AquaPaddleOn();
            }
            //grass
            if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
            {
                GrassPaddleOn();
            }
        }
  

    }

    private void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0)
        {
            _rb.AddForce(_direction * speed);
        }    
    }

    public void FirePaddleOn()
    {
        isFirePaddle = true;
        firePaddle.SetActive(true);

        isAquaPaddle = false;
        isGrassPaddle = false;

        grassPaddle.SetActive(false);
        aquaPaddle.SetActive(false);
    }
    public void AquaPaddleOn()
    {
        isAquaPaddle = true;
        aquaPaddle.SetActive(true);

        isFirePaddle = false;
        isGrassPaddle = false;

        firePaddle.SetActive(false);
        grassPaddle.SetActive(false);
    }

    public void GrassPaddleOn()
    {
        isGrassPaddle = true;
        grassPaddle.SetActive(true);

        isFirePaddle = false;
        isAquaPaddle = false;

        firePaddle.SetActive(false);
        aquaPaddle.SetActive(false);
    }
}
