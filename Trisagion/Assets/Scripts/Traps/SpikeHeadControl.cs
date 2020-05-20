using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadControl : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform downpoint, uppoint;
    private float downy, upy;
    private Animator anim;

    float speed = 2.0f;
    public float speedRate = 1.0f;
    private bool faceup = true;
    private bool ableMove = true;
    int timeStay;
    int timeNow;
    int timeBlink;
    int nextTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        downy = downpoint.position.y;
        upy = uppoint.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timeBlink = (int)Time.time;
        if(timeBlink%2 == 0 && timeBlink>nextTime)
        {
            anim.SetBool("Blink", true);
            nextTime = timeBlink + 1;
        }

        if (ableMove)
        {
            Movement();
        }
        else
        {
            timeNow = (int)Time.time;
            if((timeNow-timeStay)>0 && (timeNow - timeStay) % 2 == 0)
            {
                ableMove = true;
            }
        }
    }

    void Movement()
    {
        if (faceup)
        {
            rb.velocity = new Vector2(0, -speed*speedRate);
            speedRate += 0.02f;
            if (transform.position.y < downy)
            {
                rb.velocity = new Vector2(0, 0);
                anim.SetBool("Hit", true);
                timeStay = (int)Time.time;
                ableMove = false;
                faceup = false;
                speedRate = 1.0f;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, speed);
            if (transform.position.y > upy)
            {
                rb.velocity = new Vector2(0, 0);
                faceup = true;
            }
        }
    }

    void ResumeBlink()
    {
        anim.SetBool("Blink", false);
    }
    void ResumeHit()
    {
        anim.SetBool("Hit", false);
    }
}
