using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanishing : MonoBehaviour
{
    public float survivalTime;
    public float blinkInterval;
    public float[] alpha;
    public int repetition;

    private float accumulatedTime;
    private bool blink = false, revert = false;
    private int count, length;

    //private Rigidbody2D rb;
    public Collider2D coll;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        accumulatedTime = 0.0f;
        length = alpha.Length;
        count = 0;
        //rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!blink)
        {
            accumulatedTime += Time.deltaTime;
            if (accumulatedTime > survivalTime)
            {
                // no longer work
                //coll.isTrigger = true;
                tag = "Untagged";
                blink = true;
                accumulatedTime = 0.0f;
            }
        }
        else
        {
            accumulatedTime += Time.deltaTime;
            if (accumulatedTime > blinkInterval)
            {
                accumulatedTime = 0.0f;
                if (!revert)
                {
                    sr.color = new Color(255, 255, 255, alpha[count++]);
                    if (count == length)
                    {
                        if (--repetition == 0)
                        {
                            Destroy(gameObject);
                        }
                        revert = true;
                    }
                }
                else
                {
                    sr.color = new Color(255, 255, 255, alpha[--count]);
                    if (count == 0)
                    {
                        revert = false;
                    }
                }
            }
        }
    }
}
