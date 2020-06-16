using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text userNameText;
    public Text userScoreText;

    public string userName;
    public int userScore;

    // Update is called once per frame
    void Update()
    {
        userNameText.text = userName;
        userScoreText.text = userScore.ToString();
    }
}
