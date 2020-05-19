using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected LayerMask playerMask, groundMask, enemyMask;
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        playerMask = DLayerMask.playerMask;
        groundMask = DLayerMask.groundMask;
        enemyMask = DLayerMask.enemyMask;
    }

    public virtual void Hurt()
    {
        animator.SetTrigger(AnimParam.Hurt);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    // 应用于敌人的射线检测
    protected RaycastHit2D Raycast(Vector2 offset, Vector2 direction, float distance, LayerMask layer)
    {
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(position + offset, direction, distance, layer);
        Debug.DrawRay(position + offset, direction * distance,Color.green);
        return hit;
    }

}
