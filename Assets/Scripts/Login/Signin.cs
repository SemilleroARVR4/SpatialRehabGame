using System.Collections;
using System.Collections.Generic;
using AirtableUnity.PX.Model;
using UnityEngine;
using TMPro;
using System;

public class Signin : MonoBehaviour
{
    public TMP_InputField email, password, confirmPassword;
    public TextMeshProUGUI userText, errorText;
    public GameObject loguedPanel, signinPanel;

    string NewRecordJson;

    public void LogUser()
    {
        StartCoroutine(GetTableRecordsCo<UserField>("Login"));
    }

    private IEnumerator GetTableRecordsCo<T>(string tableName)
    {
        yield return StartCoroutine(AirtableUnity.PX.Proxy.ListRecordsCo<T>(tableName, (records) =>
        {
            OnResponseFinish(records);
        }));
    }

    private void OnResponseFinish<T>(List<BaseRecord<T>> records)
    {
        Debug.Log("[Airtable Unity Example] - List Records: " + records?.Count);
        foreach(BaseRecord<T> user in records){
            UserField usuario = user.fields as UserField;
            if(usuario.Email.Equals(email.text,StringComparison.OrdinalIgnoreCase))
            {
                errorText.text = "El email está en uso";
                return;
            }
        }
        if(email.text.Length<1 && password.text.Length<1)
            return;
        CreateAirtableRecord();
    }


    public void CreateAirtableRecord()
    {
        NewRecordJson = @"{
                        ""fields"": {
                        ""Email"": """ + email.text + @""""
                        + @",""Password"": """ + password.text + @""""

                        + "}}";
        Debug.Log(NewRecordJson);
        CreateAirtableRecord<BaseField>("Login", NewRecordJson, null);
    }
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
        var msg = "record id: " + record?.id + "\n";
        msg += "created at: " + record?.createdTime;
        Debug.Log("[Airtable Unity] - Create Record: " + "\n" + msg);
        userText.text = "Bienvenido " + email.text;
        signinPanel.SetActive(false);
        loguedPanel.SetActive(true);
    }
}