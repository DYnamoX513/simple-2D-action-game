using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalPlatformControl : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 1.0f;
    public bool resume = false;
    float yaxis;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yaxis = transform.position.y;  
    }

    // Update is called once per frame
    void Update()
    {
        Resume();
    }

    void Resume()
    {
        if (resume)
        {
            if (transform.position.y < yaxis)
            {
                rb.velocity = new Vector2(0, speed);
            }
            else rb.velocity = new Vector2(0, -speed);
            if (Math.Abs(transform.position.y - yaxis) < 0.1f)
            {
                transform.position = new Vector3(transform.position.x, yaxis, 0);
                rb.velocity = new Vector2(0, 0);
                resume = false;
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            resume = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            resume = true;
        }
    }
}
