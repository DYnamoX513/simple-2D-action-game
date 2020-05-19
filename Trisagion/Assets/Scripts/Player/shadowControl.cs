using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowControl : MonoBehaviour
{
    Transform player;

    SpriteRenderer thisSprite;
    SpriteRenderer playerSprite;

    Color color;

    [Space]
    [Header("Time")]
    float activeTime = 1;
    float activeStart;

    [Space]
    [Header("degree")]
    float alpha;
    float alphaSet = 1.0f;//初始值
    float alphaMultiplier = 0.9f;


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;

        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(0.7f, 0.7f, 1, alpha);
        thisSprite.color = color;
        if(Time.time>= activeStart + activeTime)
        {
            //return
            shadowPoolControl.instance.ReturnPool(this.gameObject);
        }
    }
}
