using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public MeshRenderer model;
    
    Color originalColor;

    private void Start()
    {
        originalColor = model.materials[1].color;
    }
     public void OnPointerEnter()
    {
        model.materials[1].color = Color.white;
    }
    public void OnPointerExit()
    {
        model.materials[1].color = originalColor;
    }

    public void OnPointerClick()
    {
        FindObjectOfType<TutorialController>().hasKey = true;
        gameObject.SetActive(false);
    }
}
