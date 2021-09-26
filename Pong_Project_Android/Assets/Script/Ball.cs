using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    private Object _impactRef;
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

    public GameObject luzAmarela;
    public GameObject luzVerde;
    public GameObject luzAzul;
    public GameObject luzBranca;


    //sons
    public AudioClip fireBallClip;
    public AudioClip aquaBallClip;
    public AudioClip grassBallClip;
    public AudioClip glassBallClip;
    public AudioClip magicBallClip;
    public AudioClip hitClip;

    private void Awake()
    {
        _fireBall.SetActive(false);
        _aquaBall.SetActive(false);
        _grassBall.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
        _impactRef = Resources.Load("Ball_Impact");
        luzAmarela.SetActive(false);
        luzAzul.SetActive(false);
        luzVerde.SetActive(false);
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
        _rb.velocity = Vector2.zero;
        _rb.position = Vector3.zero;
        MagicOff();
    }

    //Adiciona força quando colide com alguma superficie
    public void AddForce(Vector2 force)
    {
        
        _rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitEffect = (GameObject)Instantiate(_impactRef);
        hitEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Som de IMPACTO
        if(this.isAquaBall == false && this.isFireBall == false && this.isGrassBall == false)
        {
            AudioManager.instance.GlassBall(glassBallClip);
        }
        else
        {
            AudioManager.instance.GlassBall(magicBallClip);
        }
       
        //Collision com player
        if (collision.gameObject.CompareTag("Player Paddle"))
        {
            
            StartCoroutine(IsPlayerTouch());
            var playerP = collision.gameObject.GetComponent<Player_Paddle>();
            if (playerP.isFirePaddle == true)
            {
                if (this.isAquaBall == true && this.playerTouch == false)
                {
                    AudioManager.instance.BolaMagic(fireBallClip);
                    FireBallOn();
                    playerP.TomouDano();
                }
                else 
                {
                    AudioManager.instance.BolaMagic(fireBallClip);
                    FireBallOn(); 
                }

            }
            if (playerP.isAquaPaddle == true)
            {
                if (this.isGrassBall == true && playerTouch == false)
                {
                    AudioManager.instance.BolaMagic(aquaBallClip);
                    AquaBallOn();
                    playerP.TomouDano();
                }
                else 
                {
                    AudioManager.instance.BolaMagic(aquaBallClip);
                    AquaBallOn(); 
                }

            }
            if (playerP.isGrassPaddle == true)
            {
                if (this.isFireBall == true && this.playerTouch == false)
                {
                    AudioManager.instance.BolaMagic(grassBallClip);
                    GrassBallOn();
                    playerP.TomouDano();
                }
                else 
                {
                    AudioManager.instance.BolaMagic(grassBallClip);
                    GrassBallOn(); 
                }
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
                    AudioManager.instance.BolaMagic(fireBallClip);
                    FireBallOn();
                    compterP.TomouDano();
                }
                else
                {
                    AudioManager.instance.BolaMagic(fireBallClip);
                    FireBallOn(); 
                }

            }
            if (compterP.isAquaPaddle == true)
            {
                if (this.isGrassBall == true && playerTouch == true)
                {
                    AudioManager.instance.BolaMagic(aquaBallClip);
                    AquaBallOn();
                    compterP.TomouDano();
                }
                else
                {
                    AudioManager.instance.BolaMagic(aquaBallClip);
                    AquaBallOn(); 
                }

            }
            if (compterP.isGrassPaddle == true)
            {
                if (this.isFireBall == true && playerTouch == true)
                {
                    AudioManager.instance.BolaMagic(grassBallClip);
                    GrassBallOn();
                    compterP.TomouDano();
                }
                else 
                {
                    AudioManager.instance.BolaMagic(grassBallClip);
                    GrassBallOn(); 
                }
            }
            else if (compterP.isFirePaddle == false && compterP.isGrassPaddle == false && compterP.isAquaPaddle == false)
            {
                if (this.isFireBall == true || this.isGrassBall == true || this.isAquaBall == true && playerTouch == true)
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
        luzBranca.SetActive(false);
        luzAzul.SetActive(false);
        luzVerde.SetActive(false);
        luzAmarela.SetActive(true);

        _fireBall.gameObject.SetActive(true);
        isFireBall = true;

        _aquaBall.gameObject.SetActive(false);
        isAquaBall = false;

        _grassBall.gameObject.SetActive(false);
         isGrassBall = false;
    }
     public void AquaBallOn()
        {
        luzBranca.SetActive(false);
        luzAzul.SetActive(true);
        luzVerde.SetActive(false);
        luzAmarela.SetActive(false);

        _fireBall.gameObject.SetActive(false);
        isFireBall = false;

        _aquaBall.gameObject.SetActive(true);
        isAquaBall = true;

        _grassBall.gameObject.SetActive(false);
        isGrassBall = false;
        }

       public void GrassBallOn()
       {
        luzBranca.SetActive(false);
        luzAzul.SetActive(false);
        luzVerde.SetActive(true);
        luzAmarela.SetActive(false);

        _grassBall.gameObject.SetActive(true);
            isGrassBall = true;

            _fireBall.gameObject.SetActive(false);
            isFireBall = false;

            _aquaBall.gameObject.SetActive(false);
            isAquaBall = false;
       }
       public void MagicOff()
       {
        luzBranca.SetActive(true);
        luzAzul.SetActive(false);
        luzVerde.SetActive(false);
        luzAmarela.SetActive(false);

            _grassBall.gameObject.SetActive(false);
            isGrassBall = false;

            _fireBall.gameObject.SetActive(false);
            isFireBall = false;

            _aquaBall.gameObject.SetActive(false);
            isAquaBall = false;
       }


}

