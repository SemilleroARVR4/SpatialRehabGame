using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{
    public Transform point;

    NavMeshAgent nma;
    LineRenderer lr;
    private void Start() {
        nma = GetComponent<NavMeshAgent>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update() {
        if(nma.path.corners.Length>1)
        {
            lr.positionCount = nma.path.corners.Length;
            lr.SetPositions(nma.path.corners);
        }

        if(Input.GetKeyDown("space"))
            IrA();
    }

    void IrA(){
        nma.SetDestination(point.transform.position);
    }
}
