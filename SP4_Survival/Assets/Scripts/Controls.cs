using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    #region Singlton
    public static Controls instance;

    void Awake() 
    {
        if(instance != null)
        {
            Debug.Log("MOre tham one instances of controls are existaing");
        } 
        instance = this; 
    }
    #endregion

    // Fire Aime and Peek.    
    /*
    public KeyCode Fire;
    public KeyCode PeekLeft;
    public KeyCode PeekRight;
    public KeyCode Aim; 
    */

    public KeyCode Run;
    public KeyCode meleeAttack;
    public KeyCode Jump;
    public KeyCode Inventory;
    public KeyCode PlaceObject;
    public KeyCode PickUP;
    public float mouseSensitivityX;
    public float mouseSensitivityY;

}
