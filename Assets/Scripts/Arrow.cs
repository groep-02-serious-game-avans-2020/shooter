﻿using System.Collections;
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
        if(collision.collider.tag != "Arrow")
        {
        hitObject = true;
        Stick();
        }

        Debug.Log(collision.gameObject.name);
    }

    private void Stick()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}