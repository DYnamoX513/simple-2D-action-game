using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk : Enemy
{
    public float attackRange, feetOffset, groundDistance, speed;
    public float interval, stagnationTime;
    public int multipleBullet;
    public float bullerInterval;

    private bool ready;
    private int direction = 1;
    private Rigidbody2D rb;
    private float stagnationCount;

    [Space]
    public GameObject bullet;
    public float bulletVelocity;
    public Transform firePoint;

    [Header("deprecated value")]
    public Vector3 initialRelativePos;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        if (transform.localScale.x > 0)
        {
            direction = -1;
        }
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        Hostility();
    }

    void Hostility()
    {
        if (ready)
        {
            RaycastHit2D findPlayerH = Raycast(new Vector2(0, 0.4f), Vector2.right * direction, attackRange, playerMask);
            RaycastHit2D findPlayerL = Raycast(new Vector2(0, -0.4f), Vector2.right * direction, attackRange, playerMask);
            if (findPlayerH || findPlayerL)
            {
                ready = false;
                animator.ResetTrigger(AnimParam.Run);
                animator.SetTrigger(AnimParam.Attack);
                stagnationCount = stagnationTime;
            }
            else
            {
                if (stagnationCount < stagnationTime)
                {
                    rb.velocity = Vector2.zero;
                    stagnationCount += Time.deltaTime;
                    return;
                }
                RaycastHit2D leftGroundCheck = Raycast(new Vector2(-feetOffset, 0), Vector2.down, groundDistance, groundMask);
                RaycastHit2D rightGroundCheck = Raycast(new Vector2(feetOffset, 0), Vector2.down, groundDistance, groundMask);

                RaycastHit2D leftObjectCheck = Raycast(new Vector2(-feetOffset, 0), Vector2.left, 0.1f, groundMask | enemyMask);
                RaycastHit2D rightObjectCheck = Raycast(new Vector2(feetOffset, 0), Vector2.right, 0.1f, groundMask | enemyMask);


                if (direction == -1)
                {
                    if (leftGroundCheck && !leftObjectCheck)
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                        animator.SetTrigger(AnimParam.Run);
                    }
                    else if (rightGroundCheck && !rightObjectCheck)
                    {
                        direction = 1;
                        transform.localScale = new Vector3(-1, 1, 1);
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                        animator.ResetTrigger(AnimParam.Run);
                        animator.SetTrigger(AnimParam.Idle);
                        stagnationCount = 0.0f;
                    }
                }
                else
                {
                    if (rightGroundCheck && !rightObjectCheck)
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                        animator.SetTrigger(AnimParam.Run);
                    }
                    else if (leftGroundCheck && !leftObjectCheck)
                    {
                        direction = -1;
                        transform.localScale = new Vector3(1, 1, 1);
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                        animator.ResetTrigger(AnimParam.Run);
                        animator.SetTrigger(AnimParam.Idle);
                        stagnationCount = 0.0f;
                    }
                }
            }
        }
    }

    void Fire()
    {
        StartCoroutine(MultiBullet(multipleBullet, bullerInterval));
    }

    void Cooldown()
    {
        animator.SetTrigger(AnimParam.Idle);
        StartCoroutine(Prepare());
    }

    IEnumerator Prepare()
    {
        yield return new WaitForSeconds(interval);
        ready = true;
    }

    IEnumerator MultiBullet(int number, float interval)
    {
        for(int i = 0; i < number; i++)
        {
            GameObject _bullet = Instantiate(bullet);
            _bullet.transform.localScale = new Vector3(-direction, 1, 1);
            _bullet.transform.position = firePoint.position;
            _bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletVelocity * direction, 0);
            yield return new WaitForSeconds(interval);
        }
    }

}
