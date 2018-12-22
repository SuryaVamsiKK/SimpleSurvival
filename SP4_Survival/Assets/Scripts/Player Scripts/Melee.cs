using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public MeleeWeapons weaponHolding;
    public LayerMask layerMask = 1 << 8;
    public Transform weponHolder;
    public GameObject weoponHoldingOBJ;

    // Start is called before the first frame update
    void Start ()
    {
        weoponHoldingOBJ = new GameObject();
        weoponHoldingOBJ = Instantiate(weaponHolding.Wepon, weponHolder);
    }

    // Update is called once per frame
    void Update () 
    {
        debugMelee ();

        if (!GetComponent<PlayerStats>().InventoryStatus && !GameObject.FindGameObjectWithTag("Inventory").GetComponent<CraftingV2>().placed)
        {
            RaycastHit hit;
            if (Input.GetKeyDown(GetComponent<Controls>().meleeAttack))
            {
                if (Physics.Raycast(transform.GetChild(1).position, transform.GetChild(1).TransformDirection(Vector3.forward), out hit, weaponHolding.meleeRange, layerMask))
                {
                    if (hit.transform.gameObject.tag == "Resource")
                    {
                        hit.transform.GetComponent<HarvestableFunctionality>().health -= weaponHolding.damage;
                        hit.transform.GetComponent<HarvestableFunctionality>().GiveResources();
                    }
                }
                weoponHoldingOBJ.GetComponent<Animator>().SetBool("Attacked", true);

            }
            if (Input.GetKeyDown(GetComponent<Controls>().PickUP))
            {
                if (Physics.Raycast(transform.GetChild(1).position, transform.GetChild(1).TransformDirection(Vector3.forward), out hit, weaponHolding.meleeRange, layerMask))
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
        if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange, layerMask)) {
            Debug.DrawRay (hit.point, transform.GetChild (1).TransformDirection (Vector3.forward) * weaponHolding.meleeRange, Color.green);
        }
    }
}