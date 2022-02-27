using System.Collections;
using System.Collections.Generic;
using AirtableUnity.PX.Model;
using UnityEngine;
using TMPro;
using System;

public class Login : MonoBehaviour
{
    public TMP_InputField email, password;
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
            if(usuario.Email.Equals(email.text,StringComparison.OrdinalIgnoreCase) && usuario.Password.Equals(password.text,StringComparison.OrdinalIgnoreCase))
            {
                userText.text = "Bienvenido " + usuario.Email;
                userText.transform.parent.gameObject.SetActive(true);
                return;
            }
        }
        loginPanel.SetActive(true);
        errorText.text = "Nombre de usuario o contraseña erróneos";
    }
}