using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    public float speed, groundDistance, feetOffset;
    public float[] scaleFromSmallToLarge;

    private int health;
    private float calculatedFeetOffset;
    private Rigidbody2D rb;
    private int direction;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        health = scaleFromSmallToLarge.Length-1;
        direction = transform.localScale.x > 0 ? -1 : 1;
        float scale = scaleFromSmallToLarge[health];
        transform.localScale = new Vector3(-direction * scale, scale, scale);
        calculatedFeetOffset = feetOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-calculatedFeetOffset, 0), Vector2.down, groundDistance, groundMask);
        RaycastHit2D rightCheck = Raycast(new Vector2(calculatedFeetOffset, 0), Vector2.down, groundDistance, groundMask);
        if (leftCheck && rightCheck)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }else if (leftCheck)
        {
            direction = -1;
            float scale = scaleFromSmallToLarge[health];
            transform.localScale = new Vector3(scale, scale, scale);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }else if (rightCheck)
        {
            direction = 1;
            float scale = scaleFromSmallToLarge[health];
            transform.localScale = new Vector3(-scale, scale, scale);
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    public override void Hurt()
    {
        base.Hurt();
    }

    void GenerateParticle()
    {
        health--;
        if (health < 0)
        {
            Destroy(gameObject);
            return;
        }
        tag = "Untagged";
        float scale = scaleFromSmallToLarge[health];
        transform.localScale = new Vector3(-direction * scale, scale, scale);
        calculatedFeetOffset = feetOffset * scale / scaleFromSmallToLarge[health + 1];
    }

    void EndHurting()
    {
        animator.SetBool(AnimParam.Idle, true);
    }

    void Vulnerable()
    {
        tag = "Enemy";
        animator.SetBool(AnimParam.Idle, false);
    }
}
