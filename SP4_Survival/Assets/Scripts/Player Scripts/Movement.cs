using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float gravity;
    Vector3 moveDirection;
    public GameObject Cam;
    public float camHeight;
    PlayerStats mainStats;

    
    void Start()
    {
        mainStats = this.transform.parent.GetComponent<PlayerStats>();
        this.transform.position = GameObject.FindGameObjectWithTag("Disease").transform.position + new Vector3(0, 8f, 0);
    }

    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        CharacterController controller = GetComponent<CharacterController>();
        if (!mainStats.InventoryStatus)
        {
            this.transform.rotation = Quaternion.Euler(0, Cam.transform.eulerAngles.y, 0);

            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (Input.GetKeyDown(Controls.instance.Jump) && mainStats.stamina > mainStats.amountOfReductionOnJump)
                {
                    moveDirection.y = jumpSpeed;
                }

                if (Input.GetKey(Controls.instance.Run) && mainStats.stamina > 0)
                {
                    moveDirection *= speed * 0.5f;
                }

            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        Cam.transform.position = this.transform.position + new Vector3(0, camHeight, 0);
    }
}
