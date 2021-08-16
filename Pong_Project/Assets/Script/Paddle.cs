using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected Rigidbody2D _rb;

    public float speed = 10.0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddlePosition()
    {
        _rb.position = new Vector2(_rb.position.x, 0.0f);
        _rb.velocity = Vector2.zero;
    }

}
