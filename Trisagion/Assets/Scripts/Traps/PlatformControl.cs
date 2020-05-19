using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    public bool resume = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
           Resume();
    }

    void Resume()
    {
        if (resume)
        {
            if(transform.rotation.z<0) transform.Rotate(new Vector3(0, 0, 0.5f));
            else transform.Rotate(new Vector3(0, 0, -0.5f));
        }
        if (transform.rotation.z < 0.01f && transform.rotation.z > -0.01f)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            resume = false;
        }
        else resume = true;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            resume = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            resume = true;
        }
    }
}
