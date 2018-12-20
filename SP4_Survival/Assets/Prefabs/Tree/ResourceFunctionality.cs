using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFunctionality : MonoBehaviour
{
    public Resources item;
    public float destoryTime;
    public float health;
    public Vector3 dir;
    public int amountTobeGiven;

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
        
    public void GiveResources()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Inventory");
        bool wasPickedup = false;

        if (g.GetComponent<inventoryReaderV2>().inventoryOpen)
        {
            wasPickedup = InventoryV2.instance.Add(item, amountTobeGiven);
        }   
        if (!wasPickedup)
        {
            Debug.Log("Dropped 10 wood");
        }
    }
}
