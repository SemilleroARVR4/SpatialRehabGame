using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lista : MonoBehaviour
{
    public Transform interactableObjects, panelList;
    public GameObject listItem;
    Animator anim;
    GameObject listObject;
    AudioSource audioSource;
    // Update is called once per frame
    public List<Pickable2> objetos = new List<Pickable2>();
    public Pickable2 actualObject;
    int actualIndex = 0;
    private void Start(){
        anim = gameObject.GetComponentInChildren<Animator>();
        listObject = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();

        objetos.AddRange(interactableObjects.GetComponentsInChildren<Pickable2>());

        GameObject firstItem = Instantiate(listItem,panelList.position,Quaternion.identity,panelList);
        actualObject = objetos[actualIndex];
        firstItem.GetComponent<TextMeshProUGUI>().text = "-" + actualObject.DisplayName;
    }

    public void OnPickedObject()
    {
        panelList.GetComponentsInChildren<TextMeshProUGUI>()[actualIndex].fontStyle = FontStyles.Strikethrough;

        if(actualIndex+1 < objetos.Count)
        {
            actualIndex++;
            GameObject item = Instantiate(listItem,panelList.position,panelList.rotation,panelList);
            actualObject =objetos[actualIndex];
            FindObjectOfType<PathFinder>().point = actualObject.transform;
            item.GetComponent<TextMeshProUGUI>().text = "-" + actualObject.DisplayName;
            FindObjectOfType<Controller>().Register(actualIndex);
        }
        else
        {
            FindObjectOfType<Controller>().OnFinished();
            FindObjectOfType<Controller>().Register(actualIndex+1);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("BtnLista")) {
            // Reverse animation play
            if (!listObject.activeSelf)
            {
                anim.SetTrigger("Up");
                audioSource.Play();
            }
            else
            {
                anim.SetTrigger("Down");
                audioSource.Play();
            }
        }
    }
}
