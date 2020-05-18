using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_AngryPig : Enemy
{
    private Rigidbody2D rb;
    private CapsuleCollider2D bodyColl;
    private EdgeCollider2D headColl;

    public Transform leftpoint, rightpoint;

    private float leftx, rightx;

    public float speed, runningSpeed;
    public int interval;
    private int initInterval;

    private bool faceleft = true, rage, invincible;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        bodyColl = GetComponent<CapsuleCollider2D>();
        headColl = GetComponent<EdgeCollider2D>();

        initInterval = interval;

        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (interval <= 0)
        {
            Movement();
        }
        else if (rage)
        {
            Movement_rage();
        }
    }

    void Movement()
    {
        animator.SetBool(AnimParam.Idle, false);
        animator.SetBool(AnimParam.Walk, true);
        if (faceleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(0, rb.velocity.y);
                faceleft = false;
                animator.SetBool(AnimParam.Idle, true);
                animator.SetBool(AnimParam.Walk, false);
                interval = initInterval;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(0, rb.velocity.y);
                faceleft = true;
                animator.SetBool(AnimParam.Idle, true);
                animator.SetBool(AnimParam.Walk, false);
                interval = initInterval;
            }
        }
    }

    void Movement_rage()
    {
        if (faceleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(0, rb.velocity.y);
                faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(0, rb.velocity.y);
                faceleft = true;
            }
        }
    }

    public override void Hurt()
    {
        if (!invincible)
        {
            if (rage)
            {
                rb.isKinematic = true;
                bodyColl.enabled = false;
                headColl.enabled = false;
            }
            base.Hurt();
            rb.velocity = Vector2.zero;
            // Never stop to relax
            interval = 1;
            // Stand for a while
            rage = false;
            // More difficult
            invincible = true;
        }
    }

    void PerpareWalk()
    {
        interval--;
    }

    void Rage()
    {
        rage = true;
        invincible = false;
        animator.ResetTrigger(AnimParam.Hurt);
        animator.SetBool(AnimParam.Run, true);
    }

}
