using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private BoxCollider boxCollider;
    private bool hitObject = false;
    private GameObject[] bonusTargets;

    public AudioSource arrowAudioSource;
    public AudioClip arrowHitSound;
    public AudioClip arrowHitTargetSound;

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

        // If the arrow hits another arrow, ignore its boxcollider
        else
        {
            Physics.IgnoreCollision(boxCollider, collision.collider, true);
        }

        //If a bonus target exists, send coords from last arrowcollision
        bonusTargets = GameObject.FindGameObjectsWithTag("bonus-target");
        if (bonusTargets.Length > 0)
        {
            foreach (GameObject target in bonusTargets)
            {
                TargetAI ai = target.GetComponent<TargetAI>();
                ai.StartMovement(collision.contacts[0].point);
            }
        }

        //If the arrow collided with a bonus target, remove the floating arrow
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("bonus"))
        {
            Destroy(gameObject);
        }
    }

    private void Stick()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        //Remove hitboxes to avoid colliding with a moving target.
        boxCollider.enabled = false;
    }
}
