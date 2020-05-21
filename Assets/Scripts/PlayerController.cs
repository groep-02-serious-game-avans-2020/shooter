using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	private Rigidbody rb;

    public delegate void OnCollision();
    public event OnCollision OnCollisionEvent;

	public float speed;

    private float moveHorizontal;
    private float moveVertical;

	void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(float moveHorizontal, float moveVertical)
    {
        this.moveHorizontal = moveHorizontal;
        this.moveVertical = moveVertical;
    }

    void FixedUpdate()
    {
    	Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    	rb.AddForce(movement * speed);
    }
}