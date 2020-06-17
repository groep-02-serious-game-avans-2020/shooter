using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAI : MonoBehaviour
{
    private Vector3 targetLocation;
    private bool startMovement;
    private System.Random random = new System.Random();
    private GameObject targetController;
    private int score;

    public GameObject target;
    public Transform targetCenter;
    public int maxScore = 100;
    public int minScore = 5;
    public Collider targetCollider;
    public GameObject scoreNumberTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        targetController = GameObject.Find("Target Controller");
        GetRandomVector();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(transform.position.x, targetLocation.x) && Mathf.Approximately(transform.position.z, targetLocation.z))
        {
            GetRandomVector();
        }
        float step = 5.0f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
        transform.LookAt(Camera.main.transform.position);
    }

    public void StartMovement(Vector3 arrowCollision)
    {
        if (arrowCollision.y < 5)
        {
            arrowCollision.y = 5;
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
        targetLocation = new Vector3(random.Next(-11, 11) + targetController.transform.position.x, random.Next(-11, 11) + targetController.transform.position.y, random.Next(-11, 11) + targetController.transform.position.z);
        //Avoid the target from disappearing underground
        if (targetLocation.y < 5)
        {
            targetLocation.y = 5;
        }
    }

    private int calculateScore(float distance)
    {
        // Calculate the actual score
        float actualScore = maxScore - distance;

        // If the score is below the minimum, return the minimum else return the actual score
        if (actualScore < minScore)
        {
            return minScore;
        }
        else
        {
            return Mathf.RoundToInt(actualScore);
        }
    }

    public void RegisterCollision(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.name.Contains("arrow"))
        {
            // Get the Vector3 of the arrow hitpoint GameComponent inside the Arrow
            Vector3 hitPosition = collisionObject.transform.Find("arrow_hitpoint").transform.position;

            // Measure the distance between the arrow hitpoint and the target center
            float distance = Vector3.Distance(targetCenter.position, hitPosition) * 100;

            score = calculateScore(distance);

            SpawnScoreNumberText(score, hitPosition);

            GameManager.singleton.IncreaseScore(score);
            Destroy(target);
        }
    }

    public void SpawnScoreNumberText(int score, Vector3 position)
    {
        Quaternion rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180, 0);

        GameObject scoreNumber = Instantiate(scoreNumberTextPrefab, position, rotation);
        scoreNumber.GetComponent<TextMesh>().text = score.ToString();
    }
}
