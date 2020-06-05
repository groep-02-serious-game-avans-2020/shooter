using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Target[] targets;
    public Text questionText;
    public Text surveyNameText;
    public Text totalScoreText;
    public Text currentScoreText;

    private SurveyModel currentSurvey;
    private QuestionModel currentQuestion;
    private List<AnswerModel> answers;
    private int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Mock survey
        SurveyModel survey = new SurveyModel
        {
            _ID = "123456789",
            title = "Evaluatie vak Gamification 2020",
            resultUrl = "localhost:3000/api/answer/",
            Questions = new List<QuestionModel>() 
            {
                new QuestionModel
                {
                    questionNumber = 1,
                    question = "De samenwerking met de Informaticastudenten verliep goed.",
                    textAnswer = false
                },
                new QuestionModel
                {
                    questionNumber = 2,
                    question = "De leraren vertoonde deskundigheid en wisten goed waar ze het over hadden.",
                    textAnswer = false
                },
                new QuestionModel
                {
                    questionNumber = 2,
                    question = "De leraren waren in staat te helpen bij problemen binnen het project.",
                    textAnswer = false
                }
            }
        };
        answers = new List<AnswerModel>();

        currentSurvey = survey;
        currentQuestion = currentSurvey.Questions[0];
    }

    // Update is called once per frame
    void Update()
    {
        surveyNameText.text = currentSurvey.title;
        questionText.text = currentQuestion.question;
        totalScoreText.text = totalScore.ToString();
    }

    public void IncreaseScore(int amount)
    {
        totalScore += amount;
        currentScoreText.text = amount.ToString();
    }

    void AnswerQuestion(string textAnswer)
    {
        answers.Add(new AnswerModel
        {
            questionNumber = currentQuestion.questionNumber,
            textAnswer = textAnswer
        });
        NextQuestion();
    }

    void AnswerQuestion(int numberAnswer)
    {
        answers.Add(new AnswerModel
        {
            questionNumber = currentQuestion.questionNumber,
            numberAnswer = numberAnswer
        });
        NextQuestion();
    }

    void NextQuestion()
    {
        // Check if there are more questions
        if(currentSurvey.Questions[currentQuestion.questionNumber] != null)
        {
            currentQuestion = currentSurvey.Questions[currentQuestion.questionNumber];
        } else
        {
            SurveyDone();
        }
    }

    void SurveyDone()
    {
        // TODO: Send answers to backend
    }
}
