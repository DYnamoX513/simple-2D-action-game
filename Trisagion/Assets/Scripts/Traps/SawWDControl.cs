using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawWDControl : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform downpoint, uppoint;
    private float downy, upy;

    float speed = 5.0f;
    private bool faceup = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        downy = downpoint.position.y;
        upy = uppoint.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceup)
        {
            rb.velocity = new Vector2(0, -speed);
            if (transform.position.y < downy)
            {
                rb.velocity = new Vector2(0, 0);
                faceup = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, speed);
            if (transform.position.y > upy)
            {
                rb.velocity = new Vector2(0, 0);
                faceup = true;
            }
        }
    }
}
