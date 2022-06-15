using System.Collections;
using System.Collections.Generic;
using AirtableUnity.PX.Model;
using UnityEngine;
using TMPro;
using System;

public class AnswersRegister : MonoBehaviour
{
    string NewRecordJson;
    public void CreateAirtableRecord(string user, int correctAnswers)
    {
        NewRecordJson = @"{
                        ""fields"": {
                        ""User"": """  +        user       + @""""
                        + @",""CorrectAnswers"": " +   correctAnswers    + @""
                        + "}}";
        Debug.Log(NewRecordJson);
        CreateAirtableRecord<BaseField>("Preguntas", NewRecordJson, null);
    }
    //(int)avgTime+"."+ (int)((avgTime/1)*10) 
    
    private void CreateAirtableRecord<T>(string tableName, string newData, Action<BaseRecord<T>> callback)
    {
        StartCoroutine(CreateRecordCo(tableName, newData, callback));
    }

    private IEnumerator CreateRecordCo<T>(string tableName, string newData, Action<BaseRecord<T>> callback)
    {
        yield return StartCoroutine(AirtableUnity.PX.Proxy.CreateRecordCo<T>(tableName, newData, (createdRecord) =>
        {
            OnResponseCreateFinish(createdRecord);
        }));
    }

    private void OnResponseCreateFinish<T>(BaseRecord<T> record)
    {
        Controller.registerID = record?.id;
        var msg = "record id: " + record?.id + "\n";
        msg += "created at: " + record?.createdTime;
        Debug.Log("[Airtable Unity] - Create Record: " + "\n" + msg);
        FindObjectOfType<CardboardCtrler>().Invoke("StopXR",1);
    }
}