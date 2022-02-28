using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    public Transform point, spawnPoint;

    NavMeshAgent nma;
    LineRenderer lr;
    public GameObject signalObject;

    NavMeshPath path;
    private void Start() {
        nma = GetComponent<NavMeshAgent>();
        lr = GetComponentInChildren<LineRenderer>();
        path = new NavMeshPath();
    }

    private void Update() {
        if(Input.GetButtonDown("DebugBtn"))
            CreateSignal();
    }

    private void FixedUpdate() {
        CalculatePath();
        if(path.corners.Length>1)
        {
            lr.positionCount = path.corners.Length;
            lr.SetPositions(path.corners);
        }
    }

    void CreateSignal()
    {
        GameObject signal = Instantiate(signalObject,spawnPoint.position,Quaternion.identity);
        signal.GetComponent<NavMeshAgent>().SetPath(path);
    }

    void CalculatePath(){

        NavMesh.SamplePosition(point.position, out NavMeshHit hit, 2.0f, NavMesh.AllAreas);
        NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);
    }
}
