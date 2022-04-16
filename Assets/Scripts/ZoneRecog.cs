using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneRecog : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log(gameObject.name);
        if(other.name != "ZeroPoint")
            FindObjectOfType<Controller>().ZoneRegister(other.name);
        else
            FindObjectOfType<Lista>().ReturnedToStart();
    }
}
