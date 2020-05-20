using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Radish : Enemy
{
    [Header("Basic")]
    public Transform upPoint;
    public Transform downPoint;
    public Collider2D headColl;
    public Transform left;
    public Transform right;
    public GameObject leftLeaf, rightLeaf;
    private float upY, downY;

    [Header("Speed")]
    public float maxFloatingSpeed;
    private float minFloatingSpeed;
    public float lowerThreshold;
    public float stagnationTime;
    public float runningSpeed;

    private bool faceUp, inTheAir, stagnate, onTheGround;
    private int faceLeft;
    private float midpoint, stagnationCount;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        upY = upPoint.position.y;
        downY = downPoint.position.y;
        Destroy(upPoint.gameObject);
        Destroy(downPoint.gameObject);

        midpoint = (upY - downY) / 2 + downY;
        minFloatingSpeed = lowerThreshold * maxFloatingSpeed;
        faceLeft = transform.localScale.x > 0 ? -1 : 1;
        runningSpeed = faceLeft * runningSpeed;
        inTheAir = faceUp = true;
        stagnate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inTheAir)
        {
            Floating();
        }else if (onTheGround)
        {
            rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
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
    }

    public override void Hurt()
    {
        base.Hurt();
        gameObject.tag = "Untagged";
        if (inTheAir)
        {
            GameObject leaf1 = Instantiate(leftLeaf);
            GameObject leaf2 = Instantiate(rightLeaf);
            leaf1.transform.position = left.position;
            leaf2.transform.position = right.position;
            leaf1.GetComponent<Rigidbody2D>().velocity = new Vector2(-maxFloatingSpeed/2, maxFloatingSpeed/2);
            leaf2.GetComponent<Rigidbody2D>().velocity = new Vector2(maxFloatingSpeed/2, maxFloatingSpeed/2);
            Destroy(left.gameObject);
            Destroy(right.gameObject);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Falling()
    {
        gameObject.tag = "Enemy";
        headColl.isTrigger = false;
        if (inTheAir)
        {
            inTheAir = false;
            rb.gravityScale = 1;
            animator.SetBool(AnimParam.Idle, true);
        }
        else
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrian" && !inTheAir)
        {
            animator.SetBool(AnimParam.Run, true);
            animator.SetBool(AnimParam.Idle, false);
            headColl.isTrigger = onTheGround = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
