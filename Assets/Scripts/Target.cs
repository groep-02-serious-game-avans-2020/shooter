using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform targetCenter;
    public int maxScore = 100;
    public int minScore = 5;
    public Collider targetCollider;
    //public GameManager gameManager;

    private TextMesh targetNumberText;
    private int score;
    private int targetNumber;

    // Start is called before the first frame update
    void Start()
    {
        targetNumberText = gameObject.GetComponentInChildren<TextMesh>();
        targetNumberText.text = targetNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTargetNumber()
    {
        return targetNumber;
    }

    public void SetTargetNumber(int number)
    {
        targetNumber = number;
    }

    private int calculateScore(float distance)
    {
        // Calculate the actual score
        float actualScore = maxScore - distance;

        // If the score is below the minimum, return the minimum else return the actual score
        if (actualScore < minScore)
        {
            return minScore;
        } else
        {
            return Mathf.RoundToInt(actualScore);
        }
    }

    public void RegisterCollision(TargetCollisionHandler child, Collision collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.name.Contains("arrow"))
        {
            // Get the Vector3 of the arrow hitpoint GameComponent inside the Arrow
            Vector3 hitPosition = collisionObject.transform.Find("arrow_hitpoint").transform.position;

            // Measure the distance between the arrow hitpoint and the target center
            float distance = Vector3.Distance(targetCenter.position, hitPosition) * 100;

            score = calculateScore(distance);

            Debug.Log("Target " + targetNumber + " hit, score is " + score);

            //gameManager.IncreaseScore(score);
            //gameManager.AnswerQuestion(0);
        }
    }
} 
