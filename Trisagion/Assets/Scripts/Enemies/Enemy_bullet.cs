using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{
    public GameObject fragment1, fragment2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject f1 = Instantiate(fragment1);
        GameObject f2 = Instantiate(fragment2);
        f1.transform.position = gameObject.transform.position + new Vector3(0, 0.1f, 0);
        f2.transform.position = gameObject.transform.position + new Vector3(0, -0.1f, 0);
        f1.GetComponent<Rigidbody2D>().velocity = 
            f2.GetComponent<Rigidbody2D>().velocity = 
            gameObject.GetComponent<Rigidbody2D>().velocity;
        
        Destroy(gameObject);
    }

}
