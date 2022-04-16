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
    private void Start() {
        string opt1 = "Encima de la nevera";
        string opt2 = "Sobre un mezón";
        string opt3 = "En el suelo";
        string opt4 = "No había una manzana";
        Question addedQuestion = new Question("¿En qué lugar estaba la Manzana recogida?",opt1,opt2,opt3,opt4,2);
        questions.Add(addedQuestion);

        opt1 = "En la terraza";
        opt2 = "En un baño del segundo piso";
        opt3 = "En el baño del primer piso";
        opt4 = "En la sala";
        addedQuestion = new Question("¿En qué lugar estaba el papel higienico?",opt1,opt2,opt3,opt4,3);
        questions.Add(addedQuestion);

        opt1 = "En la cocina sobre la estufa";
        opt2 = "En la cocina en el suelo";
        opt3 = "En un lugar del segundo piso";
        opt4 = "Afuera de la casa";
        addedQuestion = new Question("¿En qué lugar estaba el Sartén?",opt1,opt2,opt3,opt4,1);
        questions.Add(addedQuestion);

        opt1 = "Sobre una cama";
        opt2 = "En el patio con la ropa";
        opt3 = "En el estudio del primer piso";
        opt4 = "Sobre un escritorio en una habitación";
        addedQuestion = new Question("¿En qué lugar estaban las llaves?",opt1,opt2,opt3,opt4,4);
        questions.Add(addedQuestion);

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

    public Question(string questiontxt, string opt1, string opt2, string opt3, string opt4, int correctIndex)
    {
        question = questiontxt;
        options[0] = opt1;
        options[1] = opt2;
        options[2] = opt3;
        options[3] = opt4;
        this.correctIndex = correctIndex;
    }
}