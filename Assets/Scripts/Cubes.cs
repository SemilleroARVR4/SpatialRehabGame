using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Positionate();
    }

    // Update is called once per frame
    void OnPointerClick()
    {
        Positionate();
    }

    void Positionate(){
        int i = Random.Range(0,8);
        transform.position = FindObjectsOfType<ObjectPositiion>()[i].transform.position;
    }
}
