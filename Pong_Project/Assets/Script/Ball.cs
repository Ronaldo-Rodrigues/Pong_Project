using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;

    public float speed = 200.0f;

    [SerializeField]
    private GameObject _fireBall;
    public bool isFireBall = false;


    [SerializeField]
    private GameObject _aquaBall;
    public bool isAquaBall = false;


    [SerializeField]
    private GameObject _grassBall;
    public bool isGrassBall = false;

    public bool playerTouch;


    private void Awake()
    {
        _fireBall.SetActive(false);
        _aquaBall.SetActive(false);
        _grassBall.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
    }


    //força randomica inicial para movimento da bola
    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(1.0f, 0.5f);

        Vector2 direction = new Vector2(x, y);
        _rb.AddForce(direction * speed);
    }

    public void ResetPosition()
    {
        _rb.position = Vector3.zero;
        _rb.velocity = Vector2.zero;
        MagicOff();
    }

    //Adiciona força quando colide com alguma superficie
    public void AddForce(Vector2 force)
    {
        _rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision com player
        if (collision.gameObject.CompareTag("Player Paddle"))
        {
            
            StartCoroutine(IsPlayerTouch());
            var playerP = collision.gameObject.GetComponent<Player_Paddle>();
            if (playerP.isFirePaddle == true)
            {
                if (this.isAquaBall == true && this.playerTouch == false)
                {
                    FireBallOn();
                    playerP.TomouDano();
                }
                else { FireBallOn(); }

            }
            if (playerP.isAquaPaddle == true)
            {
                if (this.isGrassBall == true && playerTouch == false)
                {
                    AquaBallOn();
                    playerP.TomouDano();
                }
                else { AquaBallOn(); }

            }
            if (playerP.isGrassPaddle == true)
            {
                if (this.isFireBall == true && this.playerTouch == false)
                {
                    GrassBallOn();
                    playerP.TomouDano();
                }
                else { GrassBallOn(); }
            }
            else if (playerP.isFirePaddle == false && playerP.isGrassPaddle == false && playerP.isAquaPaddle == false)
            {
               
                if(this.isFireBall == true || this.isGrassBall == true || this.isAquaBall == true && playerTouch == false)
                {
                    playerP.TomouDano();
                    MagicOff();
                }
                else { MagicOff(); }
            }
        }
        //Collision com computer
        if (collision.gameObject.CompareTag("Computer Paddle"))
        {
            
            StartCoroutine(IsNotPlayerTouch());
            var compterP = collision.gameObject.GetComponent<Computer_Padle>();
            if (compterP.isFirePaddle == true)
            {
                if (this.isAquaBall == true && playerTouch == true)
                {
                    FireBallOn();
                    compterP.TomouDano();
                }
                else { FireBallOn(); }

            }
            if (compterP.isAquaPaddle == true)
            {
                if (this.isGrassBall == true && playerTouch == true)
                {
                    AquaBallOn();
                    compterP.TomouDano();
                }
                else { AquaBallOn(); }

            }
            if (compterP.isGrassPaddle == true)
            {
                if (this.isFireBall == true && playerTouch == true)
                {
                    GrassBallOn();
                    compterP.TomouDano();
                }
                else { GrassBallOn(); }
            }
            else if (compterP.isFirePaddle == false && compterP.isGrassPaddle == false && compterP.isAquaPaddle == false)
            {
                if (this.isFireBall == true || this.isGrassBall == true || this.isAquaBall == true && playerTouch == false)
                {
                    compterP.TomouDano();
                    MagicOff();
                }
                else { MagicOff(); }
            }
        }
        
    }

    IEnumerator IsPlayerTouch()
    {
        yield return new WaitForSeconds(0.5f);
        playerTouch = true;
    }
    IEnumerator IsNotPlayerTouch()
    {
        yield return new WaitForSeconds(0.5f);
        playerTouch = false;
    }
    public void FireBallOn()
    {
        _fireBall.gameObject.SetActive(true);
        isFireBall = true;

        _aquaBall.gameObject.SetActive(false);
        isAquaBall = false;

        _grassBall.gameObject.SetActive(false);
         isGrassBall = false;
    }
     public void AquaBallOn()
        {
            _fireBall.gameObject.SetActive(false);
            isFireBall = false;

            _aquaBall.gameObject.SetActive(true);
            isAquaBall = true;

            _grassBall.gameObject.SetActive(false);
            isGrassBall = false;
        }

       public void GrassBallOn()
       {
            _grassBall.gameObject.SetActive(true);
            isGrassBall = true;

            _fireBall.gameObject.SetActive(false);
            isFireBall = false;

            _aquaBall.gameObject.SetActive(false);
            isAquaBall = false;
       }
       public void MagicOff()
       {
            _grassBall.gameObject.SetActive(false);
            isGrassBall = false;

            _fireBall.gameObject.SetActive(false);
            isFireBall = false;

            _aquaBall.gameObject.SetActive(false);
            isAquaBall = false;
       }


}

