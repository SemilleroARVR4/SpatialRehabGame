using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Timer timer;
    public float helpLevel = 0;
    bool timerHelpTrigger1, timerHelpTrigger2;

    public GameObject txtUI;

    public List<string> tempEnteredZones = new List<string>();

    public List<Task> tasks = new List<Task>();

    public static string username = "TestUser";

    public static string registerID = "";

    private void Start() {
        Timer.Initialize();
    }
    
    private void Update() {

        if(!timerHelpTrigger1 && Timer.GetTimeBetweenObjects() > 45)
        {
            timerHelpTrigger1 = true;
            helpLevel += 1;
            FindObjectOfType<PathFinder>().InvokeRepeating("CreateSignal",0,10);
            txtUI.GetComponent<TMPro.TextMeshPro>().text = "<b>Ayuda:</b>\nLas señales te guian al objeto";
            txtUI.SetActive(true);
            Invoke(nameof(HideTextUI),6);
        }

        if(!timerHelpTrigger2 && Timer.GetTimeBetweenObjects() > 60)
        {
            timerHelpTrigger2 = true;
            helpLevel += 1;
            FindObjectOfType<PathFinder>().CancelInvoke("CreateSignal");
            FindObjectOfType<PathFinder>().GetComponentInChildren<LineRenderer>().enabled = true;
            txtUI.GetComponent<TMPro.TextMeshPro>().text = "<b>Ayuda:</b>\nLa linea del suelo te guia al objeto, síguela";
            txtUI.SetActive(true);
            Invoke(nameof(HideTextUI),10);
        }
    }

    public void Register(int objectIndex)
    {
        tasks.Add(new Task(tempEnteredZones,Timer.OnPickedObject()));
        tempEnteredZones = new List<string>();
        timerHelpTrigger1 = false;
        timerHelpTrigger2 = false;
        FindObjectOfType<PathFinder>().CancelInvoke();
        FindObjectOfType<PathFinder>().GetComponentInChildren<LineRenderer>().enabled = false;
    }

    public void ZoneRegister(string name)
    {
        if(tempEnteredZones.Count == 0 || !tempEnteredZones.Contains(name))
            tempEnteredZones.Add(name);
    }

    void HideTextUI()
    {
        txtUI.SetActive(false);
    }

    public void OnFinished()
    {
        txtUI.GetComponent<TMPro.TextMeshPro>().text = "¡Tareas Completadas!";
        txtUI.SetActive(true);
        float avgTime = 0;
        int totalPlaces = 0;
        foreach(Task t in tasks)
        {
            avgTime += t.time;
            totalPlaces += t.enteredZones.Count;
        }
        avgTime= avgTime/tasks.Count;
        FindObjectOfType<Register>().CreateAirtableRecord(username, avgTime, totalPlaces);
    }

    public void LoadForm()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Preguntas");
    }
}

public class Task
{
    public List<string> enteredZones;
    public float time;

    public Task(List<string> zones, float timeIn)
    {
        enteredZones = zones;
        time = timeIn;
    }
}
