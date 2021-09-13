using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.SerializableAttribute]
public class Player_Paddle : Paddle
{
    public EventTrigger.TriggerEvent scoreTrigger;

    private Vector2 _direction;

    

    public Animator maguinhoAnim;

   
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

    //Luzes
    public GameObject luzAzul;
    public GameObject luzVerde;
    public GameObject luzAmarela;



    private void Start()
    {
       
        PlayerPaddleReset();
        
    }
    void Update()
    {
        //Movimento e animação
        if (Input.GetKey(KeyCode.UpArrow))
        {
           _direction = Vector2.up;
            maguinhoAnim.SetBool("isDown", false);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction = Vector2.down;            
        }
        else
        {
             _direction = Vector2.zero;
        }
       //animação de maguinho
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            maguinhoAnim.SetBool("isDown", true);
        }
        if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            maguinhoAnim.SetBool("isDown", false);
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

    
    
        if(this.transform.position.y < -0.1f)
        {
           
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

        luzAzul.SetActive(false);
        luzVerde.SetActive(false);
        luzAmarela.SetActive(true);

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

        luzAzul.SetActive(true);
        luzVerde.SetActive(false);
        luzAmarela.SetActive(false);

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

        luzAzul.SetActive(false);
        luzVerde.SetActive(true);
        luzAmarela.SetActive(false);

        isGrassPaddle = true;
        grassPaddle.SetActive(true);

        isFirePaddle = false;
        isAquaPaddle = false;

        firePaddle.SetActive(false);
        aquaPaddle.SetActive(false);
    }

    public void MagicPaddleOff()
    {

        luzAzul.SetActive(false);
        luzVerde.SetActive(false);
        luzAmarela.SetActive(false);

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
        gm.ShakeIt();
        AudioManager.instance.Dano(danoClip);
        
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
