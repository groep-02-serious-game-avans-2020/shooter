using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rigidbody;
    bool hitObject = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hitObject)
        {
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.name.Contains("arrow"))
        {
        hitObject = true;
        Stick();
        }

        if(collision.gameObject.name.Contains("target"))
        {
            Debug.Log("Target " + collision.gameObject.name + " hit");
        }

        // Debug.Log(collision.gameObject.name);
    }

    private void Stick()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
