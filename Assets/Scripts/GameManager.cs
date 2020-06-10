using Assets.Models;
using System;
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
        currentScoreText.text = "";
        answers = new List<AnswerModel>();
        currentSurvey = DataManager.singleton.currentSurvey;
        currentQuestion = currentSurvey.questions[0];
        Debug.Log(currentSurvey.title);
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
        Debug.Log("Survey done, sending answers to server...", this);
        if (DataManager.singleton.SubmitAnswers(answers, currentSurvey._id))
        {
            Debug.Log("Answers sent to server, redirecting to main menu", this);
            SceneManager.LoadScene("main_menu");
        }
        else
        {
            // Error
        }
    }
}
