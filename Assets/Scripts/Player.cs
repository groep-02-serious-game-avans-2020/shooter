using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float movementSpeed = 3;
    public GameObject scoreboard;
    private bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard.SetActive(false);
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    { 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        characterController.SimpleMove(movement * movementSpeed);

        if (Input.GetKeyDown("escape"))
        {
            if (toggle)
            {
                scoreboard.SetActive(false);
                toggle = false;
            } 
            else
            {
                scoreboard.SetActive(true);
                toggle = true;
            }
        }

        //if(movement.magnitude > 0)
        //{
        //    Quaternion rotation = Quaternion.LookRotation(movement);
        //    transform.rotation = rotation;
        //}
    }
}
