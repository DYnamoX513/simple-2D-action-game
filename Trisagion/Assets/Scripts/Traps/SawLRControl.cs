using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawLRControl : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    private float leftx, rightx;

    float speed = 5.0f;
    private bool faceleft = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceleft)
        {
            rb.velocity = new Vector2(-speed, 0);
            if (transform.position.x < leftx)
            {
                rb.velocity = new Vector2(0, 0);
                faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(0, 0);
                faceleft = true;
            }
        }
    }
}
