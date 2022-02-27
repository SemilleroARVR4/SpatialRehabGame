using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float lastTime = 0;

    public static void Initialize()
    {
        lastTime = Time.realtimeSinceStartup;
    }
    
    public static float GetTimeBetweenObjects()
    {
        return Time.realtimeSinceStartup - lastTime;
    }

    public static float OnPickedObject()
    {
        float returnedTime = Time.realtimeSinceStartup - lastTime;
        lastTime = Time.realtimeSinceStartup;
        return returnedTime;
    }
}
