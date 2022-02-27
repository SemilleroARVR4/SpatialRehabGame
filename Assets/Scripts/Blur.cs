using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blur : MonoBehaviour
{

    Camera cam;
    public Shader imgEf;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.SetReplacementShader(imgEf,"");
    }

    // Update is called once per frame
    void Update()
    {
        cam.RenderWithShader(imgEf,"");
    }
}
