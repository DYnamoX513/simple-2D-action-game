using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turtle : Enemy
{
    public Collider2D spike;
    public float spikeDelay, range;
    public int health;

    private float delayCount;
    private bool hostile;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hostile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hostile)
        {
            Hostility();
        }
        else
        {
            Alert();
        }
    }

    void Alert()
    {
        RaycastHit2D findPlayerLeft = Raycast(Vector2.zero, Vector2.left, range, playerMask);
        RaycastHit2D findPlayerRight = Raycast(Vector2.zero, Vector2.right, range, playerMask);

        if (findPlayerLeft || findPlayerRight)
        {
            hostile = true;
            animator.SetBool(AnimParam.Idle, false);
            animator.SetBool(AnimParam.Hostile, true);
        }
    }

    void Hostility()
    {
        RaycastHit2D findPlayerLeft = Raycast(Vector2.zero, Vector2.left, range, playerMask);
        RaycastHit2D findPlayerRight = Raycast(Vector2.zero, Vector2.right, range, playerMask);

        if (findPlayerLeft || findPlayerRight)
        {
            delayCount = 0.0f;
            return;
        }
        else
        {
            delayCount += Time.deltaTime;
            if (delayCount > spikeDelay)
            {
                hostile = false;
                animator.SetBool(AnimParam.Idle, false);
                animator.SetBool(AnimParam.Hostile, false);
                delayCount = 0.0f;
            }
        }
    }

    void SpikeOut()
    {
        tag = "Enemy";
        spike.enabled = true;
        EnemySoundManager.instance.Spike();
    }

    void EndOfSpikeInAndOut()
    {
        animator.SetBool(AnimParam.Idle, true);
    }

    void SpikeIn()
    {
        spike.enabled = false;
    }

    public override void Hurt()
    {
        base.Hurt();
        // innocuous
        tag = "Untagged";
    }

    void EndOfHurt()
    {
        health--;
        if (health == 0)
        {
            Death();
        }

        hostile = true;
        animator.SetBool(AnimParam.Idle, false);
        animator.SetBool(AnimParam.Hostile, true);
    }

}
