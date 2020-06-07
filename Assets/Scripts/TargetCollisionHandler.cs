using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollisionHandler : MonoBehaviour
{
    Target parentScript;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<Target>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        parentScript.RegisterCollision(collision);
    }
}
