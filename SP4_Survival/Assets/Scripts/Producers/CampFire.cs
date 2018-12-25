using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class CampFire : MonoBehaviour
{
    public Resource need = Resource.Wood;
    public Resource woodNeed = Resource.Wood;

    public float productionTime;
    public int needForOne;
    public int WoodForOne;
    public GameObject Output;
    public int resourceAmount;
    public int WoodAmount;
    public GameObject Produced;
    public GameObject Campfire;

    public bool started = false;
    void Start()
    {
        Produced = null;
    }
    
    void Update()
    {

        if (resourceAmount >= needForOne && started == false && WoodAmount >= WoodForOne)
        {
            StartCoroutine(Spwan());
            started = true;
        }

        if (resourceAmount < needForOne && WoodAmount < WoodForOne)
        {
            started = false;
            Campfire.SetActive(false);
        }

        if(GetComponent<ScriptActiivation>().interacted)
        {
            for (int i = 0; i < InventoryV2.instance.Bag.Count; i++)
            {
                if (InventoryV2.instance.Bag[i].item.typeOfResource == need)
                {
                    if (InventoryV2.instance.Bag[i].amount >= needForOne)
                    {
                        InventoryV2.instance.Bag[i].amount -= needForOne;
                        resourceAmount += needForOne;
                    }
                }
            }
            for (int i = 0; i < InventoryV2.instance.Bag.Count; i++)
            {
                if (InventoryV2.instance.Bag[i].item.typeOfResource == woodNeed)
                {
                    if (InventoryV2.instance.Bag[i].amount >= WoodForOne)
                    {
                        InventoryV2.instance.Bag[i].amount -= WoodForOne;
                        WoodAmount += WoodForOne;
                    }
                }
            }
            GetComponent<ScriptActiivation>().interacted = false;
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (this.GetComponent<ScriptActiivation>().enabled)
    //    {
    //        if (other.gameObject.tag == "Player")
    //        {
    //            if (Input.GetKeyDown(other.transform.parent.GetComponent<Controls>().PickUP))
    //            {
    //            }
    //        }
    //    }
    //}

    IEnumerator Spwan()
    {
        Campfire.SetActive(true);
        yield return new WaitForSeconds(productionTime);
        resourceAmount -= needForOne;
        WoodAmount -= WoodForOne;
        if (resourceAmount >= needForOne && WoodAmount >= WoodForOne)
        {
            StartCoroutine(Spwan());
        }
        if (Produced == null)
        {
            Produced = Instantiate(Output);
            Produced.GetComponent<PickUp>().amount = 1;
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
