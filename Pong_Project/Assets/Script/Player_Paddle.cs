using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
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
    public bool canCastFire = true;
    public bool canCastAqua = true;
    public bool canCastGrass = true;


    private void Start()
    {
        firePaddle.SetActive(false);
        aquaPaddle.SetActive(false);
        grassPaddle.SetActive(false);
    }
    void Update()
    {
        //Movimento
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
        if(canCastMagic == true && canCastFire == true)
        {
            //Fire
            if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
            {
                FirePaddleOn();
                StartCoroutine(FireMagicWait());
            }
        }
        if(canCastMagic == true && canCastAqua == true)
        {
            //Aqua
            if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            {
                AquaPaddleOn();
                StartCoroutine(AquaMagicWait());
            }
        }
        if(canCastMagic == true && canCastGrass == true)
        {
            //grass
            if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
            {
                GrassPaddleOn();
                StartCoroutine(GrassMagicWait());
            }
        }
        else { return; }
  

    }

    private void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0)
        {
            _rb.AddForce(_direction * speed);
        }    
    }

    IEnumerator FireMagicWait()
    {
        //cooldown de fire paddle
        canCastMagic = false;
        canCastFire = false;
        yield return new WaitForSeconds(4);
        canCastMagic = true;
        MagicPaddleOff();
        yield return new WaitForSeconds(2);
        canCastFire = true;
    }
    IEnumerator AquaMagicWait()
    {   //cooldown de aquapaddle
        canCastMagic = false;
        canCastAqua = false;
        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        canCastMagic = true;
        yield return new WaitForSeconds(2);
        canCastAqua = true;
    }
    IEnumerator GrassMagicWait()
    {   //cooldown de grass paddle
        canCastMagic = false;
        canCastGrass = false;
        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        canCastMagic = true;
        yield return new WaitForSeconds(2);
        canCastGrass = true;
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

    public void MagicPaddleOff()
    {
        isGrassPaddle = false;
        grassPaddle.SetActive(false);

        isAquaPaddle = false;
        aquaPaddle.SetActive(false);

        isFirePaddle = false;
        firePaddle.SetActive(false);
    }
}
