using UnityEngine;

[CreateAssetMenu(fileName = "Spørgsmål", menuName = "Tilføj spørgsmål/Stort spørgsmål,Lille spørgsmål")]
public class QuestionsSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

}
