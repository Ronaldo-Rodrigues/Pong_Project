using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Computer_Padle : Paddle
{
    public EventTrigger.TriggerEvent scoreTrigger;

    public Rigidbody2D ball;
    public GameObject floatingPoints;
    public GameObject playerMagic;

    public GameObject prolongPaddle;
    public GameObject prolongBroke;
    public int hpPaddle = 3;

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

    //sons
    public AudioClip danoClip;
    public AudioClip magicActiveClip;
    public AudioClip shatter1Clip;
    public AudioClip shatter2Clip;

    private void Start()
    {
       
        playerMagic = GameObject.FindGameObjectWithTag("Player Paddle");
        ResetCompPaddle();

    }
    private void Update()
    {
        if(playerMagic)
        { 
            var bola = ball.GetComponent<Ball>();
            
            //se o padler puder jogar magia quando a bola se aproxmar
            if (bola.transform.position.x <= -1 && bola.isAquaBall == false && bola.isFireBall == false && bola.isGrassBall == false)
            {
                //rebate zero
                if (canCastMagic == true)
                {
                    var castRandom = Random.Range(1, 3);
                    
                    if (castRandom == 1)
                    {
                        StartCoroutine(GrassCombatOn());
                    }
                    if (castRandom == 2)
                    {
                        StartCoroutine(AquaCombatOn());
                    }
                    if (castRandom == 3)
                    {
                        StartCoroutine(FireCombatOn());
                    }
                }
                else { return; }
            }

            //Contra-Ataque 
            var qualMagica = playerMagic.GetComponent<Player_Paddle>();

            if (qualMagica.isFirePaddle == true)
           {
              if(this.canCastMagic == true && canCastAqua == true)
              {
                   StartCoroutine(AquaCombatOn());
              }
              else if(this.canCastMagic == true && canCastAqua == false && canCastFire == true)
              {
                   StartCoroutine(FireCombatOn());
              }
              else { return; }

           }
           if (qualMagica.isAquaPaddle == true)
           {
               if (this.canCastMagic == true && canCastGrass == true)
               {
                   StartCoroutine(GrassCombatOn());
               }
               else if (this.canCastMagic == true && canCastGrass == false && canCastAqua == true)
               {
                   StartCoroutine(AquaCombatOn());
               }
               else { return; }
           }
           if (qualMagica.isGrassPaddle == true)
           {
               if (this.canCastMagic == true && canCastFire == true)
               {
                   StartCoroutine(FireCombatOn());
               }
               else if (this.canCastMagic == true && canCastFire == false && canCastGrass == true)
               {
                   StartCoroutine(GrassCombatOn());
               }
               else { return; }

           }
        }

    }

        private void FixedUpdate()
    {

        if (this.ball.velocity.x < 0.0f)
        {

            //Faz o paddle ir de encontro com a bola
            if (this.ball.position.y > this.transform.position.y)
            {
                _rb.AddForce(Vector2.up * speed);
            }
            else if (this.ball.position.y < this.transform.position.y)
            {
                _rb.AddForce(Vector2.down * speed);
            }

            //retorna o paddle para o centro
            else
            {
                if (this.transform.position.y > 0.0f)
                {
                    _rb.AddForce(Vector2.down * speed);
                }
                else if (this.transform.position.y < 0.0f)
                {
                    _rb.AddForce(Vector2.up * speed);
                }
            }
        }

        if (this.ball.position.x < -1.0f)
        {
           
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
    IEnumerator FireCombatOn()
    {
        canCastMagic = false;
        yield return new WaitForSeconds(1);
        FirePaddleOn();
        
        canCastFire = false;

        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        yield return new WaitForSeconds(8);
        canCastFire = true;
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

    IEnumerator AquaCombatOn()
    {
        canCastMagic = false;
        yield return new WaitForSeconds(1);
        AquaPaddleOn();
        canCastAqua = false;
        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        yield return new WaitForSeconds(8);
        canCastAqua = true;
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

    IEnumerator GrassCombatOn()
    {
        canCastMagic = false;
        yield return new WaitForSeconds(1);
        GrassPaddleOn();
        
        canCastGrass = false;
        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        yield return new WaitForSeconds(9);
        canCastGrass = true;
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

        canCastMagic = true;
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
            AudioManager.instance.Shatter(shatter2Clip);
        }
        hpPaddle--;
        
    }

    public void ResetCompPaddle()
    {
        hpPaddle = 3;
        prolongPaddle.SetActive(true);
        prolongBroke.SetActive(false);
        this.transform.position = new Vector2(transform.position.x, 0);
        MagicPaddleOff();
    }
}
