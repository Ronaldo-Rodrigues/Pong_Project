using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Padle : Paddle
{

    public Rigidbody2D ball;
    public GameObject floatingPoints;

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
