using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private bool canShoot;
    private float xRotation = 0f;

    public float movementSpeed = 3;
    public float mouseSensitivity = 100f;
    public Camera playerCamera;
    public Bow bow;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            Cursor.lockState = CursorLockMode.Locked;
            MovePlayer();
            MoveCamera();
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        characterController.SimpleMove(movement * movementSpeed);
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
        bow.SetCanShoot(canShoot);
    }
}
