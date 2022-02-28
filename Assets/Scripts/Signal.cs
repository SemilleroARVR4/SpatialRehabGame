using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Signal : MonoBehaviour
{
    NavMeshAgent nav;

    private void Start() {
        nav = GetComponent<NavMeshAgent>();
    }
    private void Update() {
        if(!nav.hasPath)
            Destroy(gameObject);
    }
}
