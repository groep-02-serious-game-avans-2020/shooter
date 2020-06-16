using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargetCollisionHandler : MonoBehaviour
{
    TargetAI parentScript;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<TargetAI>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        parentScript.RegisterCollision(collision);
    }
}
