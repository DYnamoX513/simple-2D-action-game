using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Radish : Enemy
{
    [Header("Basic")]
    public Transform upPoint, downPoint;
    private float upY, downY;

    [Header("Speed")]
    public float maxFloatingSpeed;
    private float minFloatingSpeed;
    public float lowerThreshold;
    public float stagnationTime;
    public float runningSpeed;

    private bool faceLeft, faceUp, inTheAir, stagnate;
    private float midpoint, stagnationCount;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        upY = upPoint.position.y;
        downY = downPoint.position.y;
        Destroy(upPoint.gameObject);
        Destroy(downPoint.gameObject);

        midpoint = (upY - downY) / 2 + downY;
        minFloatingSpeed = lowerThreshold * maxFloatingSpeed;
        faceLeft = transform.localScale.x > 0;
        inTheAir = faceUp = true;
        stagnate = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (inTheAir)
        {
            Floating();
        }
    }

    void Floating()
    {
        if (stagnate)
        {
            stagnationCount += Time.deltaTime;
            if (stagnationCount > stagnationTime)
            {
                stagnate = false;
                stagnationCount = 0.0f;
            }
            return;
        }
        float coefficient = 1 - Math.Abs(transform.position.y - midpoint) / midpoint;

        float calculatedSpeed;
        if (coefficient > 1)
        {
            calculatedSpeed = maxFloatingSpeed;
        }
        else if (coefficient < lowerThreshold)
        {
            calculatedSpeed = minFloatingSpeed;
        }
        else
        {
            calculatedSpeed = coefficient * maxFloatingSpeed;
        }

        if (faceUp)
        {
            rb.velocity = new Vector2(0, calculatedSpeed);
            if (transform.position.y > upY)
            {
                rb.velocity = Vector2.zero;
                faceUp = false;
                stagnate = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, -calculatedSpeed);
            if (transform.position.y < downY)
            {
                rb.velocity = Vector2.zero;
                faceUp = true;
                stagnate = true;
            }
        }

        // being blocked by terrian

    }

    public override void Hurt()
    {
        //base.Hurt();
    }
}
