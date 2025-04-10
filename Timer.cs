using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    float timerValue;
    [SerializeField] float timeToCompleteQuestion = 10f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;

    public bool isAnsweringquestion;
    public float fillFraction;
    public bool loadNextQuestion;

    // Update is called once per frame

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringquestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                isAnsweringquestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }


            else
            {
                isAnsweringquestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }

        Debug.Log(timerValue + " " + isAnsweringquestion + " = " + fillFraction);

    }
}
