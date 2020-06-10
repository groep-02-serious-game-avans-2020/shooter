using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform targetCenter;
    public int maxScore = 100;
    public int minScore = 5;
    public Collider targetCollider;

    public TextMesh targetNumberText;
    public GameObject scoreNumberTextPrefab;

    private int score;
    private int targetNumber;

    // Start is called before the first frame update
    void Start()
    {
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

            Debug.Log("Target " + targetNumber + " hit, score is " + score);

            GameManager.singleton.IncreaseScore(score);
            GameManager.singleton.AnswerQuestion(targetNumber);
        }
    }

    public void SpawnScoreNumberText(int score, Vector3 position)
    {
        Quaternion rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180, 0);

        GameObject scoreNumber = Instantiate(scoreNumberTextPrefab, position, rotation);
        scoreNumber.GetComponent<TextMesh>().text = score.ToString();
    }
}
