using Assets.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform targetCenter;
    public int maxScore = 100;
    public int minScore = 5;
    public GameManager gameManager;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("arrow"))
        {
            // Get the Vector3 of the arrow hitpoint GameComponent inside the Arrow
            Vector3 hitPosition = collision.gameObject.transform.Find("arrow_hitpoint").transform.position;

            // Measure the distance between the arrow hitpoint and the target center
            float distance = Vector3.Distance(targetCenter.position, hitPosition) * 100;

            score = calculateScore(distance);

            gameManager.IncreaseScore(score);
        }
    }

    private int calculateScore(float distance)
    {
        // Calculate the actual score
        float actualScore = maxScore - distance;

        // If the score is below the minimum, return the minimum else return the actual score
        if (score < minScore)
        {
            return minScore;
        } else
        {
            return Mathf.RoundToInt(actualScore);
        }
    }
} 
