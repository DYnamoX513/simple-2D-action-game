using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectionControl : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
    }

    void SwitchAnim()
    {
        anim.SetBool("destory", true);
    }

    void DestoryThis()
    {
        Destroy(gameObject);
    }
}
