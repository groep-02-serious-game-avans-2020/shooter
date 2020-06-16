using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAI : MonoBehaviour
{

    public GameObject target;
    private Vector3 targetLocation;
    private bool startMovement;
    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(transform.position.x, targetLocation.x) && Mathf.Approximately(transform.position.y, targetLocation.y)) {
            GetRandomVector();
        }
        float step = 5.0f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
    }

    public void StartMovement(Vector3 arrowCollision)
    {
        if (-arrowCollision.y < 0)
        {
            arrowCollision.y = 0;
        } 
        else
        {
            arrowCollision.y = -arrowCollision.y;
        }
        arrowCollision.x = -arrowCollision.x;
        arrowCollision.z = -arrowCollision.z;
        //Set target
        targetLocation = arrowCollision;
        startMovement = true;
    }

    private void GetRandomVector()
    {
        targetLocation = new Vector3(random.Next(26), random.Next(26), random.Next(26));
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
