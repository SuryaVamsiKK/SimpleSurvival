using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public MeleeWeapons weaponHolding;
    public Transform weponHolder;
    public GameObject weoponHoldingOBJ;

    public Wearable maskWearing;
    public Transform maskHolder;
    public GameObject maskWearingOBJ;

    public GameObject DebugPoint;
    RaycastHit hitpos;

    public LayerMask masks;


    void Start ()
    {
        weoponHoldingOBJ = new GameObject();
        weoponHoldingOBJ = Instantiate(weaponHolding.Wepon, weponHolder);

        maskWearingOBJ = new GameObject();
        maskWearingOBJ = Instantiate(maskWearing.Msk, maskHolder);
    }
    
    void Update () 
    {
        //if (Physics.Raycast(transform.GetChild(1).position, transform.GetChild(1).TransformDirection(Vector3.forward), out hitpos, weaponHolding.meleeRange, masks))
        //{
        //    DebugPoint.transform.position = hitpos.point;
        //    DebugPoint.transform.forward = hitpos.normal;
        //}

        debugMelee ();

        if(weoponHoldingOBJ.tag != weaponHolding.Wepon.tag)
        {
            Destroy(weoponHoldingOBJ);
            weoponHoldingOBJ = Instantiate(weaponHolding.Wepon, weponHolder);
        }

        if (maskWearingOBJ.tag != maskWearing.Msk.tag)
        {
            Destroy(maskWearingOBJ);
            weoponHoldingOBJ = Instantiate(maskWearing.Msk, maskHolder);
        }

        if (!GetComponent<PlayerStats>().InventoryStatus && !GameObject.FindGameObjectWithTag("Inventory").GetComponent<CraftingV2>().placed)
        {
            RaycastHit hit;
            if (Input.GetKeyDown(GetComponent<Controls>().meleeAttack))
            {
                if (Physics.Raycast(transform.GetChild(1).position, transform.GetChild(1).TransformDirection(Vector3.forward), out hit, weaponHolding.meleeRange, weaponHolding.AccessLevel))
                {
                    if (hit.transform.gameObject.tag == "Resource")
                    {
                        hit.transform.GetComponent<HarvestableFunctionality>().health -= weaponHolding.damage;
                        hit.transform.GetComponent<HarvestableFunctionality>().GiveResources(weaponHolding.harvestMultiplier);
                    }
                }
                weoponHoldingOBJ.GetComponent<Animator>().SetBool("Attacked", true);

            }
            if (Input.GetKeyDown(GetComponent<Controls>().PickUP))
            {
                if (Physics.Raycast(transform.GetChild(1).position, transform.GetChild(1).TransformDirection(Vector3.forward), out hit, weaponHolding.meleeRange, weaponHolding.AccessLevel))
                {
                    if (hit.transform.gameObject.tag == "Interactable")
                    {
                        hit.transform.GetComponent<ScriptActiivation>().interacted = true;
                    }
                }
            }
            if (weoponHoldingOBJ.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Melee"))
            {
                weoponHoldingOBJ.GetComponent<Animator>().SetBool("Attacked", false);
            }
        }
    }

    void debugMelee () 
    {
        RaycastHit hit;

        Debug.DrawRay(transform.GetChild(1).position, transform.GetChild(1).TransformDirection(Vector3.forward) * weaponHolding.meleeRange, Color.black);
        if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange)) {
            Debug.DrawRay (hit.point, transform.GetChild (1).TransformDirection (Vector3.forward) * weaponHolding.meleeRange, Color.green);
        }
    }
}