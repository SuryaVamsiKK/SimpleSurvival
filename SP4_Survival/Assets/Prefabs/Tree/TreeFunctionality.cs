using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFunctionality : MonoBehaviour
{
    public Resources item;
    public float destoryTime;
    public float health;
    public Vector3 dir;

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
            StartCoroutine(DestoryTree());
            dir = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).forward;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().AddForce(dir,ForceMode.Impulse);
        }
    }
    IEnumerator DestoryTree()
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(this.gameObject);
    }
        
    public void GiveWood()
    {
        //Debug.Log("Added 10 Wood");
        bool wasPickedup = InventoryV2.instance.Add(item);

        if(!wasPickedup)
        {
            Debug.Log("Dropped 10 wood");
        }
    }
}
