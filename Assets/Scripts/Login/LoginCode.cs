using System.Collections;
using System.Collections.Generic;
using AirtableUnity.PX.Model;
using UnityEngine;
using TMPro;
using System;

public class LoginCode : MonoBehaviour
{
    public TMP_InputField codigo;
    public TextMeshProUGUI userText, errorText;

    public GameObject loginPanel;

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
            if(usuario.Email.Equals(codigo.text, StringComparison.OrdinalIgnoreCase))
            {
                userText.text = "Login Correcto";
                userText.transform.parent.gameObject.SetActive(true);
                Controller.username = usuario.Email;
                return;
            }
        }
        loginPanel.SetActive(true);
        errorText.text = "ID no encontrado";
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}