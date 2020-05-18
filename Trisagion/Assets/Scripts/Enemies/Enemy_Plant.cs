using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Plant : Enemy
{
    public float attackRange;
    public int interval;
    private int intervalCount = 0;
    private int direction = 1;
    public GameObject bullet;
    public float bulletVelocity;
    public Transform firePoint;

    [Header("deprecated value")]
    public Vector3 initialRelativePos;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (transform.localScale.x < 0)
        {
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Hostility();
    }

    void Hostility()
    {
        if (intervalCount <= 0) {
            RaycastHit2D findPlayerH = Raycast(new Vector2(0, 0.4f) * direction, Vector2.left, attackRange, playerMask);
            RaycastHit2D findPlayerL = Raycast(new Vector2(0, -0.4f) * direction, Vector2.left, attackRange, playerMask);
            if (findPlayerH || findPlayerL)
            {
                intervalCount = interval;
                animator.SetTrigger(AnimParam.Attack); 
            }
        }
    }

    void Fire()
    {
        GameObject _bullet = Instantiate(bullet);
        _bullet.transform.position = firePoint.position;
        _bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletVelocity * direction, 0);
    }

    void Recharge()
    {
        intervalCount--;
    }

    void Cooldown()
    {
        animator.SetTrigger(AnimParam.Idle);
    }

    public override void Hurt(){}
}
