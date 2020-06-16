using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class for managing the bow and arrow game
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public Text questionText;
    public Text surveyNameText;
    public Text totalScoreText;
    public Text currentScoreText;
    public HighScoreList highScores;
    public GameObject highScorePrefab;
    public GameObject highScoreCanvas;
    public RectTransform highScoreSpawnPoint;
    public Player player;

    private SurveyModel currentSurvey;
    private QuestionModel currentQuestion;
    private List<AnswerModel> answers;
    private int totalScore = 0;
    private int questionsAnswered = 0;

    private void Awake()
    {
        if (singleton)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.SetCanShoot(true);
        highScoreCanvas.SetActive(false);
        currentScoreText.text = "";
        answers = new List<AnswerModel>();
        currentSurvey = DataManager.singleton.currentSurvey;
        currentQuestion = currentSurvey.questions[0];
    }

    // Update is called once per frame
    void Update()
    {
        surveyNameText.text = currentSurvey.title;
        questionText.text = currentQuestion.question;
        totalScoreText.text = "Total score: " + totalScore.ToString();
    }

    public void IncreaseScore(int amount)
    {
        totalScore += amount;
        currentScoreText.text = "Last shot: " + amount.ToString();
    }

    public void AnswerQuestion(string textAnswer)
    {
        answers.Add(new AnswerModel
        {
            questionNumber = currentQuestion.questionNumber,
            textAnswer = textAnswer
        });
        questionsAnswered += 1;
        NextQuestion();
    }

    public void AnswerQuestion(int numberAnswer)
    {
        answers.Add(new AnswerModel
        {
            questionNumber = currentQuestion.questionNumber,
            numberAnswer = numberAnswer
        });
        questionsAnswered += 1;
        NextQuestion();
    }

    private void NextQuestion()
    {
        // Check if there are more questions
        if (questionsAnswered < currentSurvey.questions.Count)
        {
            currentQuestion = currentSurvey.questions[currentQuestion.questionNumber];
        }
        else
        {
            SurveyDone();
        }
    }

    /// <summary>
    /// Sends the survey answers to the server and redirects to main menu after success
    /// </summary>
    void SurveyDone()
    {
        player.SetCanShoot(false);

        Debug.Log("Survey done, sending answers to server...", this);
        if (DataManager.singleton.SubmitAnswers(answers, currentSurvey._id, totalScore))
        {
            Debug.Log("Answers sent to server, redirecting to main menu", this);

            if (highScores.highScores == null)
            {
                LoadMainMenu();
            }

            ShowHighScores();
        }
        else
        {
            // Error
        }
    }

    void ShowHighScores()
    {
        highScoreCanvas.SetActive(true);

        if (highScores.highScores.Length > 10)
        {
            for (int i = 0; i < 9; i++)
            {
                Vector3 location = highScoreSpawnPoint.rect.position;
                //location.y += (i * 30);
                GameObject hs = Instantiate(highScorePrefab, location, highScorePrefab.transform.rotation, highScoreCanvas.transform);

                RectTransform rt = hs.GetComponent<RectTransform>();
                rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 200, rt.rect.width);
                rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 150 + (i * 30), rt.rect.height);

                HighScore hsScript = hs.GetComponent<HighScore>();
                hsScript.userName = highScores.highScores[i].userName;
                hsScript.userScore = highScores.highScores[i].score;
            }
        }
        else
        {
            for (int i = 0; i < highScores.highScores.Length; i++)
            {
                Vector3 location = highScoreSpawnPoint.rect.position;
                //location.y += (i * 30);
                GameObject hs = Instantiate(highScorePrefab, location, highScorePrefab.transform.rotation, highScoreCanvas.transform);

                RectTransform rt = hs.GetComponent<RectTransform>();
                rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 200, rt.rect.width);
                rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 150 + (i * 30), rt.rect.height);

                HighScore hsScript = hs.GetComponent<HighScore>();
                hsScript.userName = highScores.highScores[i].userName;
                hsScript.userScore = highScores.highScores[i].score;
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}
