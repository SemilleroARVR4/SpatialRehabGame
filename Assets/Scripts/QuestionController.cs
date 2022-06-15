using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestionController : MonoBehaviour
{
    public List<Question> questions = new List<Question>();
    public TextMeshProUGUI questionTxt;
    public List<TextMeshProUGUI> optionstxt;
    public GameObject questionsPanel, endPanel;

    float initTime, totalTime;

    int actualIndex = 0, correctAnswers;
    private void Start()
    {
        foreach (Pickable2 pickable in Lista.usedObjects)
        {
            Question addedQuestion = new Question(pickable.question, pickable.options, pickable.correctIndex);
            questions.Add(addedQuestion);
        }
        ShowNewQuestion();
    }

    public void OnAswer(int index)
    {
        if(questions[actualIndex].correctIndex == index)
        {
            correctAnswers++;
            Debug.Log("Correcto");
        }
        else
            Debug.Log("INCorrecto");
        
        actualIndex++;
        if(actualIndex<questions.Count)
            ShowNewQuestion();
        else
        {
            FindObjectOfType<Form>().UpdateAirtableRecord(correctAnswers);
            questionsPanel.SetActive(false);
            endPanel.SetActive(true);
            totalTime = initTime-Time.realtimeSinceStartup;
        }
    }

    public void ShowNewQuestion()
    {
        questionTxt.text = questions[actualIndex].question;
        for (int i = 0; i < 4; i++)
        {
            optionstxt[i].text = questions[actualIndex].options[i]; 
        }
    }

    public void InitTimer()
    {
        initTime = Time.realtimeSinceStartup;
    }

    public void BotonFinal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}

public class Question
{
    public string question;
    public string[] options = new string[4];
    public int correctIndex;

    public Question(string questiontxt, string[] opts, int correctIndex)
    {
        question = questiontxt;
        options = opts;
        this.correctIndex = correctIndex;
    }
}