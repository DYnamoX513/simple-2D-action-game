using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    public float speed, groundDistance, feetOffset;
    public float[] scaleFromSmallToLarge;
    public GameObject[] particleFromSmallToLarge;

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

    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        RaycastHit2D leftGroundCheck = Raycast(new Vector2(-calculatedFeetOffset, 0), Vector2.down, groundDistance, groundMask);
        RaycastHit2D rightGroundCheck = Raycast(new Vector2(calculatedFeetOffset, 0), Vector2.down, groundDistance, groundMask);

        RaycastHit2D leftObjectCheck = Raycast(new Vector2(-calculatedFeetOffset, -calculatedFeetOffset/3), Vector2.left, 0.1f, groundMask | enemyMask);
        RaycastHit2D rightObjectCheck = Raycast(new Vector2(calculatedFeetOffset, -calculatedFeetOffset/3), Vector2.right, 0.1f, groundMask | enemyMask);

        
        if(direction == -1)
        {
            if (leftGroundCheck && (!leftObjectCheck || leftObjectCheck.collider.gameObject.tag == "SlimeParticle"))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else if (rightGroundCheck && !rightObjectCheck)
            {
                direction = 1;
                float scale = scaleFromSmallToLarge[health];
                transform.localScale = new Vector3(-scale, scale, scale);
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
        else
        {
            if (rightGroundCheck && (!rightObjectCheck || rightObjectCheck.collider.gameObject.tag == "SlimeParticle"))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (leftGroundCheck && !leftObjectCheck)
            {
                direction = -1;
                float scale = scaleFromSmallToLarge[health];
                transform.localScale = new Vector3(scale, scale, scale);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
    }

    public override void Hurt()
    {
        base.Hurt();
    }

    void GenerateParticle()
    {
        EnemySoundManager.instance.SlimeParticle();
        GameObject particle = Instantiate(particleFromSmallToLarge[health]);
        particle.transform.position = transform.position;
        particle.GetComponent<Rigidbody2D>().velocity = new Vector2(-direction * Random.Range(1, 3), Random.Range(1, 3));
        StartCoroutine(ParticleAvailable(particle));
        health--;
        if (health < 0)
        {
            Death();
            return;
        }
        
        tag = "Untagged";
        float scale = scaleFromSmallToLarge[health];
        transform.localScale = new Vector3(-direction * scale, scale, scale);
        calculatedFeetOffset = feetOffset * scale / scaleFromSmallToLarge.Last();
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SlimeParticle")
        {
            EnemySoundManager.instance.SlimeLarger();
            Destroy(collision.gameObject);
            if (health < scaleFromSmallToLarge.Length - 1)
            {
                health++;
                float scale = scaleFromSmallToLarge[health];
                transform.localScale = new Vector3(-direction * scale, scale, scale);
                calculatedFeetOffset = feetOffset * scale / scaleFromSmallToLarge.Last();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SlimeParticle")
        {
            EnemySoundManager.instance.SlimeLarger();
            Destroy(collision.gameObject);
            if (health < scaleFromSmallToLarge.Length-1)
            {
                health++;
                float scale = scaleFromSmallToLarge[health];
                transform.localScale = new Vector3(-direction * scale, scale, scale);
                calculatedFeetOffset = feetOffset * scale / scaleFromSmallToLarge.Last();
            }
        }
    }


    IEnumerator ParticleAvailable(GameObject particle)
    {
        yield return new WaitForSeconds(2);
        particle.tag = "SlimeParticle";
    }
}
