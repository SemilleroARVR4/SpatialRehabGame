using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneRecog : MonoBehaviour
{
    public TextMeshPro textDebug;

    private void OnTriggerExit(Collider other) {
        textDebug.text = "Pasillo";
    }
    private void OnTriggerEnter(Collider other) {
        textDebug.text = other.name;
    }
}
