using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlueBird : Enemy
{
    [Header("Basic")]
    public int health;
    public Transform leftPoint, rightPoint, upPoint, downPoint;
    private float leftX, rightX, upY, downY;

    [Header("Direction")]
    public bool vertical;
    public bool horizontal;
    public float verticalSpeed, horizontalSpeed;

    private bool faceLeft = true, faceUp = true;
    private Rigidbody2D rb;
    private bool stagnant = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        upY = upPoint.position.y;
        downY = downPoint.position.y;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
        Destroy(upPoint.gameObject);
        Destroy(downPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (stagnant)
            return;
        if (vertical)
        {
            VerticalMovement();
        }

        if(horizontal)
        {
            HorizontalMovement();
        }
    }

    void VerticalMovement()
    {
        if (faceLeft)
        {
            rb.velocity = new Vector2(-verticalSpeed, rb.velocity.y);
            if (transform.position.x < leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(verticalSpeed, rb.velocity.y);
                faceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(verticalSpeed, rb.velocity.y);
            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(-verticalSpeed, rb.velocity.y);
                faceLeft = true;
            }
        }
    }
    void HorizontalMovement()
    {
        if (faceUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, horizontalSpeed);
            if (transform.position.y > upY)
            {
                rb.velocity = new Vector2(rb.velocity.x, -horizontalSpeed);
                faceUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -horizontalSpeed);
            if (transform.position.y < downY)
            {
                rb.velocity = new Vector2(rb.velocity.x, horizontalSpeed);
                faceUp = true;
            }
        }
    }

    public override void Hurt()
    {
        base.Hurt();
        // temporarily innocuous
        gameObject.tag = "Untagged";
        stagnant = true;
        rb.velocity = Vector2.zero;
    }

    void HealthDown()
    {
        health--;
        if (health == 0)
        {
            Destroy(gameObject);
        }
        gameObject.tag = "Enemy";
        stagnant = false;
    }
}
