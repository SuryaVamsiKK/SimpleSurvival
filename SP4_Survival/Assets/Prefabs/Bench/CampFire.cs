using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampFire : MonoBehaviour
{
    public Resource need = Resource.Wood;

    public float productionTime;
    public int needForOne;
    public GameObject Output;
    public int resourceAmount;
    public GameObject Produced;
    
    void Start()
    {
        Produced = null;
    }
    
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (this.GetComponent<ScriptActiivation>().enabled)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(other.transform.parent.GetComponent<Controls>().PickUP))
                {
                    for (int i = 0; i < InventoryV2.instance.Bag.Count; i++)
                    {
                        if (InventoryV2.instance.Bag[i].item.typeOfResource == need)
                        {
                            if (InventoryV2.instance.Bag[i].amount >= needForOne)
                            {
                                InventoryV2.instance.Bag[i].amount -= needForOne;
                                resourceAmount += needForOne;
                                if (resourceAmount >= needForOne)
                                {
                                    StartCoroutine(Spwan());
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator Spwan()
    {
        yield return new WaitForSeconds(productionTime);
        resourceAmount -= needForOne;
        if (Produced == null)
        {
            Produced = Instantiate(Output);
            Produced.GetComponent<PickUp>().amount = 1;
            Produced.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Produced.transform.position = this.transform.GetChild(1).transform.position;
            yield break;
        }

        if (Produced != null)
        {
            Produced.GetComponent<PickUp>().amount++;
            yield break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(this.transform.GetChild(1).position, 0.25f);
    }
}
