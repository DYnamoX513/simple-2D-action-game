using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    private Animator anim;
    CircleCollider2D c2D;
    int time;
    int nexttime;
    bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        c2D = GetComponent<CircleCollider2D>();
        c2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time = (int)Time.time;
        if(time%2 == 0 && time >= nexttime)
        {
            SwitchAnim();
            nexttime = time + 1;
        }
    }

    void SwitchAnim()
    {
        if (!state)
        {
            anim.SetBool("fireChange", true);
            c2D.enabled = true;
            state = true;
        }
        else
        {
            anim.SetBool("fireChange", false);
            c2D.enabled = false;
            state = false;
        }
    }
}
