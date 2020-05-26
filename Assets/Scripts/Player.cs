using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float movementSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    { 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        characterController.SimpleMove(movement * movementSpeed);

        //if(movement.magnitude > 0)
        //{
        //    Quaternion rotation = Quaternion.LookRotation(movement);
        //    transform.rotation = rotation;
        //}
    }
}
