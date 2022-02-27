using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Timer timer;
    public float helpLevel = 0;
    bool timerHelpTrigger;

    public GameObject txtFinalizado;

    public List<string> registerList = new List<string>();

    private void Start() {
        Timer.Initialize();
    }
    
    private void Update() {
        if(!timerHelpTrigger && Timer.GetTimeBetweenObjects() > 60)
        {
            timerHelpTrigger = true;
            helpLevel += 1;
        }
        if(Input.GetKeyDown("b"))
        {
            Debug.Log(Timer.lastTime);
            Debug.Log(Timer.GetTimeBetweenObjects());
            Debug.Log(Time.realtimeSinceStartup);
        }
    }

    public void Register(int objectIndex)
    {
        registerList.Add("Objeto " + objectIndex + " tiempo: " + Timer.OnPickedObject().ToString("F1"));
    }

    public void OnFinished()
    {
        txtFinalizado.SetActive(true);
    }
}
