using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.SerializableAttribute]
public class Player_Paddle : Paddle
{
    public EventTrigger.TriggerEvent scoreTrigger;

    private Vector2 _direction;

    public GameObject floatingPoints;

    public Animator maguinhoAnim;

   // public GameObject gameManager;

    public GameObject prolongPaddle;
    public GameObject prolongBroke;
    public int hpPaddle = 3;

    public bool isFirePaddle = false;
    public GameObject firePaddle;
    public GameObject fireMagicOnAnim;

    public bool isAquaPaddle = false;
    public GameObject aquaPaddle;
    public GameObject aquaMagicOnAnim;

    public bool isGrassPaddle = false;
    public GameObject grassPaddle;
    public GameObject grassMagicOnAnim;

    public GameObject paddleShreding;

    public bool canCastMagic = true;
    public bool canCastFire = true;
    public bool canCastAqua = true;
    public bool canCastGrass = true;

    public GameManager gm;

    //SONS
    public AudioClip danoClip;
    public AudioClip magicActiveClip;
    public AudioClip shatter1Clip;
    public AudioClip shatter2Clip;



    private void Start()
    {
       
        PlayerPaddleReset();
        
    }
    void Update()
    {
        //Movimento
        if (Input.GetKey(KeyCode.UpArrow))
        {
            maguinhoAnim.SetBool("isDown", false);
            _direction = Vector2.up;
            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
            maguinhoAnim.SetBool("isDown", true);
        }
        else
        {
            _direction = Vector2.zero;
        }


        // ativar as magias
        if (canCastMagic == true && canCastFire == true)
        {
            //Fire
            if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
            {
                Instantiate(fireMagicOnAnim, new Vector2 ( transform.position.x, transform.position.y), Quaternion.identity);
                FirePaddleOn();
                StartCoroutine(FireMagicWait());
            }
        }
        if(canCastMagic == true && canCastGrass == true)
        {
            //Aqua
            if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            {
                Instantiate(grassMagicOnAnim, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                GrassPaddleOn();
                StartCoroutine(GrassMagicWait());
               
            }
        }
        if(canCastMagic == true && canCastAqua == true)
        {
            //grass
            if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
            {
                Instantiate(aquaMagicOnAnim, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                AquaPaddleOn();
                StartCoroutine(AquaMagicWait());
            }
        }
        else { return; }


    
     if (this._rb.velocity.y > -2.3f)
        {
            maguinhoAnim.SetBool("isDown", false);
        }

    }

    private void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0)
        {
            _rb.AddForce(_direction * speed);
        }
        //Prolongamento de Paddle
        if (hpPaddle == 3)
        {
            prolongPaddle.SetActive(true);
            prolongBroke.SetActive(false);
        }
        if (hpPaddle == 2)
        {
            prolongBroke.SetActive(true);
            prolongPaddle.SetActive(false);
        }
        if (hpPaddle == 1)
        {
            prolongPaddle.SetActive(false);
            prolongBroke.SetActive(false);
        }
        if (hpPaddle <= 0)
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.scoreTrigger.Invoke(eventData);

        }
    }

    IEnumerator FireMagicWait()
    {
        //cooldown de fire paddle
        canCastMagic = false;
        canCastFire = false;
        
        yield return new WaitForSeconds(3);
        MagicPaddleOff();
        yield return new WaitForSeconds(7);
        canCastFire = true;
    }
    IEnumerator AquaMagicWait()
    {   //cooldown de aquapaddle
        canCastMagic = false;
        canCastAqua = false;
        yield return new WaitForSeconds(3);
        MagicPaddleOff();
        yield return new WaitForSeconds(7);
        canCastAqua = true;
    }
    IEnumerator GrassMagicWait()
    {   //cooldown de grass paddle
        canCastMagic = false;
        canCastGrass = false;
        yield return new WaitForSeconds(3);
        MagicPaddleOff();
        yield return new WaitForSeconds(7);
        canCastGrass = true;
    }

    public void FirePaddleOn()
    {
        AudioManager.instance.MagicAtive(magicActiveClip);
        isFirePaddle = true;
        firePaddle.SetActive(true);

        isAquaPaddle = false;
        isGrassPaddle = false;

        grassPaddle.SetActive(false);
        aquaPaddle.SetActive(false);
    }

    public void AquaPaddleOn()
    {
        AudioManager.instance.MagicAtive(magicActiveClip);
        isAquaPaddle = true;
        aquaPaddle.SetActive(true);

        isFirePaddle = false;
        isGrassPaddle = false;

        firePaddle.SetActive(false);
        grassPaddle.SetActive(false);
    }

    public void GrassPaddleOn()
    {
        AudioManager.instance.MagicAtive(magicActiveClip);
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

        canCastMagic = true;
    }

    public void PlayerPaddleReset()
    {
        hpPaddle = 3;
        prolongPaddle.SetActive(true);
        prolongBroke.SetActive(false);
        MagicPaddleOff();
        this.transform.position = new Vector2(transform.position.x, 0);

    }
    public void TomouDano()
    {
        AudioManager.instance.Dano(danoClip);
        Instantiate(floatingPoints, transform.position, Quaternion.identity);
        if (hpPaddle == 3)
        {
            AudioManager.instance.Shatter(shatter1Clip);
        }
        if (hpPaddle == 2)
        {
            Instantiate(paddleShreding, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            AudioManager.instance.Shatter(shatter2Clip);
        }
        hpPaddle--;
        
        
    }

}
