using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFunctionality : MonoBehaviour
{
    public Resources type;

    public float health;

    void Start()
    {
        
    }

    void Update()
    {
        FallDown();
    }

    void FallDown()
    {
        if(health <= 0)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0.1f,0.1f,0.1f),ForceMode.Impulse);
        }
    }
        
}
