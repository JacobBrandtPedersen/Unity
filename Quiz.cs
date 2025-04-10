using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using System.Collections.Generic;
using Unity.Mathematics;


public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionsSO> questions = new List<QuestionsSO>();
    QuestionsSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Buttons Color")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        DisplayQuestion();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringquestion)
        {
            DisplayAnswer(5);
            SetButtonState(false);
        }

    }

    Image buttonImage;
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";


    }

    void DisplayAnswer(int index)
    {
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswer();
        }
        else
        {

            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementQuestionSeen();
            progressBar.value++;
        }
    }

    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }

}
