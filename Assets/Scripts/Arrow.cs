using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private BoxCollider boxCollider;
    private bool hitObject = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the arrow in the right direction when it hasn't hit something yet
        if (!hitObject)
        {
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the arrow hits anything else than another arrow
        if (!collision.gameObject.name.Contains("arrow"))
        {
            hitObject = true;
            Stick();
        }
        // If the arrow hits another arrow, ignore it's boxcollider
        else
        {
            Physics.IgnoreCollision(boxCollider, collision.collider, true);
        }
    }

    private void Stick()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
