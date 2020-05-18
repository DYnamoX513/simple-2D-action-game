using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    public float speed, backSpeed;
    public float feetOffset, groundDistance;

    [Header("索敌")]
    public float range;
    public float duration, goBack;
    private float timer;
    private float initialPosX;
    private bool keepHostile;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        initialPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!keepHostile)
        {
            Hostility();
        }
        else
        {
            Dash();
        }
    }

    // Make sure the mushroom won't fall from the ground
    // And Once it finds the player, it will attack
    // After a few seconds, it will go back to it position.
    void Hostility()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-feetOffset, 0), Vector2.down, groundDistance, groundMask);
        if (leftCheck)
        {
            RaycastHit2D findPlayerLeft = Raycast(Vector2.zero, Vector2.left, range, playerMask);
            if (findPlayerLeft)
            {
                animator.SetBool(AnimParam.Run, true);
                animator.SetBool(AnimParam.Idle, false);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
                keepHostile = true;
                timer = 0;
                return;
            }
        }

        RaycastHit2D rightCheck = Raycast(new Vector2(feetOffset, 0), Vector2.down, groundDistance, groundMask);
        if (rightCheck)
        {
            RaycastHit2D findPlayerRight = Raycast(Vector2.zero, Vector2.right, range, playerMask);
            if (findPlayerRight)
            {
                animator.SetBool(AnimParam.Run, true);
                animator.SetBool(AnimParam.Idle, false);
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                keepHostile = true;
                timer = 0;
                return;
            }
        }

        if (Mathf.Abs(transform.position.x - initialPosX) < 0.5f)
        {
            animator.SetBool(AnimParam.Run, false);
            animator.SetBool(AnimParam.Idle, true);
            rb.velocity = new Vector2(0, rb.velocity.y);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer > goBack)
            {
                animator.SetBool(AnimParam.Run, true);
                animator.SetBool(AnimParam.Idle, false);
                if (transform.position.x < initialPosX)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    rb.velocity = new Vector2(backSpeed, rb.velocity.y);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    rb.velocity = new Vector2(-backSpeed, rb.velocity.x);
                }
            }
            else
            {
                animator.SetBool(AnimParam.Run, false);
                animator.SetBool(AnimParam.Idle, true);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

    }
    void Dash()
    {
        timer += Time.deltaTime;
        if(timer < duration)
        {
            RaycastHit2D leftCheck = Raycast(new Vector2(-feetOffset, 0), Vector2.down, groundDistance, groundMask);
            RaycastHit2D rightCheck = Raycast(new Vector2(feetOffset, 0), Vector2.down, groundDistance, groundMask);
            if (leftCheck && rightCheck)
            {
                rb.velocity = new Vector2(-transform.localScale.x * speed, rb.velocity.y);
            }
        }
        else
        {
            timer = 0;
            keepHostile = false;
        }
    }
}
