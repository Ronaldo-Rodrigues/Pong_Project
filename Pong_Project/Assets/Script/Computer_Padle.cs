using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Padle : Paddle
{

    public Rigidbody2D ball;
    public GameObject floatingPoints;
    public GameObject playerMagic; 

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
        playerMagic = GameObject.FindGameObjectWithTag("Player Paddle");
        firePaddle.SetActive(false);
        aquaPaddle.SetActive(false);
        grassPaddle.SetActive(false);

    }
    private void Update()
    {
        if(playerMagic)
        {
            var qualMagica = playerMagic.GetComponent<Player_Paddle>();
            //Contra-Ataque para fogo
            if (qualMagica.isFirePaddle == true)
            {
                if(this.canCastMagic == true && canCastAqua == true)
                {
                    StartCoroutine(AquaCombatOn());
                }
               if(this.canCastMagic == true && canCastAqua == false && canCastFire == true)
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
                if (this.canCastMagic == true && canCastGrass == false && canCastAqua == true)
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
                if (this.canCastMagic == true && canCastFire == false && canCastGrass == true)
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
            if (this.ball.position.y > this.transform.position.y)
            {
                _rb.AddForce(Vector2.up * speed);
            }
            else if(this.ball.position.y < this.transform.position.y)
            {
                _rb.AddForce(Vector2.down * speed);
            }
        }
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
    IEnumerator FireCombatOn()
    {
        yield return new WaitForSeconds(1);
        FirePaddleOn();
        canCastMagic = false;
        canCastFire = false;
        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        canCastMagic = true;
        yield return new WaitForSeconds(5);
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
        yield return new WaitForSeconds(1);
        AquaPaddleOn();
        canCastMagic = false;
        canCastAqua = false;
        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        canCastMagic = true;
        yield return new WaitForSeconds(5);
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
        yield return new WaitForSeconds(1);
        GrassPaddleOn();
        canCastMagic = false;
        canCastGrass = false;
        yield return new WaitForSeconds(4);
        MagicPaddleOff();
        canCastMagic = true;
        yield return new WaitForSeconds(5);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ball"))
        {
            var bola = collision.gameObject.GetComponent<Ball>();


            if (bola.isFireBall == true)
            {
                
                Instantiate(floatingPoints, transform.position, Quaternion.identity);
            }
        }
    }

}
