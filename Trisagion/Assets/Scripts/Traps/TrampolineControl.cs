using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineControl : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchAnim()
    {
        anim.SetBool("jumpping", true);
    }

    void ResumeAnim()
    {
        anim.SetBool("jumpping", false);
    }
}
