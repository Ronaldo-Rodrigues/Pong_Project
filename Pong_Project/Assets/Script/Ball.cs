using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;

    public float speed = 200.0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        AddStartingForce();
    }

    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range( 1.0f,  0.5f);

        Vector2 direction = new Vector2(x, y);
        _rb.AddForce(direction * speed); 
    }
}
