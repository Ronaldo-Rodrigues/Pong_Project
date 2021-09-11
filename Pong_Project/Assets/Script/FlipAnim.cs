using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAnim : MonoBehaviour
{

    public Transform thisAnim;
    public GameObject ball;
    public bool face = true;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        var ball1 = ball.GetComponent<Rigidbody2D>();
       if (ball1.velocity.x < 0.0f && face){
            Flip();
        }
        if (ball1.velocity.x > 0.0f && !face){
            Flip();
        }
    }
    public void Flip()
    {
        face = !face;
        Vector3 scala = thisAnim.localScale;
        scala.x *= -1;
        thisAnim.localScale = scala;
    }
}
