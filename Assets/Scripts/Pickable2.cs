using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable2 : MonoBehaviour
{   
    public string DisplayName;
     public void OnPointerEnter()
    {
        GetComponent<Outline>().enabled = true;
    }
    public void OnPointerExit()
    {
        GetComponent<Outline>().enabled = false;
    }

    public void OnPointerClick()
    {
        if(FindObjectOfType<Lista>().actualObject == this)
        {
            gameObject.SetActive(false);
            FindObjectOfType<Lista>().OnPickedObject();
        }
    }
}
