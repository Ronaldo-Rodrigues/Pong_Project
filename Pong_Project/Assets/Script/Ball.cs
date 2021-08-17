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

    private void Awake()
    {
        _fireBall.SetActive(false);
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

    }

    //Adiciona força quando colide com alguma superficie
    public void AddForce(Vector2 force)
    {
        _rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //Collision com player
        if(collision.gameObject.CompareTag("Player Paddle"))
        {
            if(collision.gameObject.GetComponent<Player_Paddle>().isFirePaddle == true)
            {
                FireBallOn();
            }
        }

        //Collision com computer
        if (collision.gameObject.CompareTag("Computer Paddle"))
        {
            
                FireBallOff();
            
        }
    }
    public void FireBallOn()
    {
        _fireBall.gameObject.SetActive(true);
        isFireBall = true;
        
    }
    public void FireBallOff()
    {
        _fireBall.gameObject.SetActive(false);
        isFireBall = false;

    }
}
