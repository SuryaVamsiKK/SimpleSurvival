using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableFunctionality : MonoBehaviour
{
    public Resources item;
    public float destoryTime;
    public float health;
    public Vector3 dir;
    public int amountTobeGiven;
    public float MaxSize;
    public float MinSize;

    void Start()
    {
        this.transform.eulerAngles = new Vector3(0, Random.Range(0,360), 0);

        float scl = Random.Range(MinSize, MaxSize);
        float oldscl = this.transform.localScale.x;
        this.transform.localScale = new Vector3(scl, scl, scl);
        health = Mathf.Round((health * scl) / oldscl);

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
        
    public void GiveResources(int HarvestMultiply)
    {
        GameObject g = GameObject.FindGameObjectWithTag("Inventory");
        bool wasPickedup = false;

        if (g.GetComponent<inventoryReaderV2>().inventoryOpen)
        {
            wasPickedup = InventoryV2.instance.Add(item, amountTobeGiven * HarvestMultiply);
        }   
        if (!wasPickedup)
        {
            Debug.Log("Dropped 10 wood");
        }
    }
}
