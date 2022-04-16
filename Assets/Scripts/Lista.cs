using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lista : MonoBehaviour
{
    public Transform interactableObjects, panelList;
    public GameObject listItem, lastListItem;
    Animator anim;
    GameObject listObject;
    AudioSource audioSource;
    // Update is called once per frame
    public List<Pickable2> objetos = new List<Pickable2>();
    public Pickable2 actualObject;
    int actualIndex = 0, pickedObjects = 0;
    public int numObjectsToFind;

    bool hasAnObject;


    private void Start(){
        anim = gameObject.GetComponentInChildren<Animator>();
        listObject = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();

        objetos.AddRange(interactableObjects.GetComponentsInChildren<Pickable2>());

        lastListItem = Instantiate(listItem,panelList.position,Quaternion.identity,panelList);
        lastListItem.GetComponent<TextMeshProUGUI>().text = "";
        SelectRandomObject();
    }

    public void OnPickedObject()
    {
        foreach(var listItem in panelList.GetComponentsInChildren<TextMeshProUGUI>())
            listItem.fontStyle = FontStyles.Strikethrough;

        lastListItem = Instantiate(listItem,panelList.position,Quaternion.identity,panelList);
        lastListItem.transform.localRotation = Quaternion.identity;
        lastListItem.GetComponent<TextMeshProUGUI>().text = "-Regresa a la puerta";
        hasAnObject = true;
        pickedObjects++;
    }

    public void ReturnedToStart()
    {
        if(pickedObjects < numObjectsToFind)
        {
            if(hasAnObject)
            {
                SelectRandomObject();
                FindObjectOfType<Controller>().Register(actualIndex);
            }
        }
        else
        {
            FindObjectOfType<Controller>().OnFinished();
        }
        hasAnObject = false;
    }

    void SelectRandomObject()
    {
        actualIndex = Random.Range(0,objetos.Count);
        actualObject = objetos[actualIndex];
        objetos.Remove(actualObject);
        FindObjectOfType<PathFinder>().point = actualObject.transform;
        lastListItem.GetComponent<TextMeshProUGUI>().text = "-" + actualObject.DisplayName;
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
