using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public MeshRenderer model;
    public Transform puerta;

    TutorialController controller;
    
    Color originalColor;

    private void Start()
    {
        originalColor = model.material.color;
        controller = FindObjectOfType<TutorialController>();
    }
     public void OnPointerEnter()
    {
        if(controller.hasKey)
            model.material.color = Color.white;
    }
    public void OnPointerExit()
    {
        model.material.color = originalColor;
    }

    public void OnPointerClick()
    {
        //if(controller.hasKey == true)
            StartCoroutine(Abrir());
    }
    IEnumerator Abrir()
    {
        FindObjectOfType<PlayerMovement>().enabled = false;
        while(puerta.localEulerAngles.y < 70)
        {
            puerta.Rotate(transform.up * Time.fixedDeltaTime * 50,Space.Self);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Casa");
    }
}
