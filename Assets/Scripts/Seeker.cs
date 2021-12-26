using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class Seeker : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;
    bool isSeek;
    private Vector3 currTarget;
    NavMeshPath path;
    void Start()
    {
        path = new NavMeshPath();
        navMeshAgent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ShowSeeker(Vector3 point)
    {
        isSeek = true;
        lineRenderer.enabled = true;
        currTarget = point;
         
        NavMesh.CalculatePath(transform.position, point, NavMesh.AllAreas , path);

        if (path.corners.Length == 0)
        {
            isSeek = false;
            lineRenderer.enabled = false;
        }   

        lineRenderer.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, path.corners[i]);
        }
        
    }
    private void Update()
    {
        if (isSeek)
        {
            NavMesh.CalculatePath(transform.position, currTarget, NavMesh.AllAreas, path);
            lineRenderer.positionCount = path.corners.Length;

            for (int i = 0; i < path.corners.Length; i++)
            {
                lineRenderer.SetPosition(i, path.corners[i]);
            }
            lineRenderer.SetPosition(0, path.corners[0]);

            if (navMeshAgent.velocity.magnitude <= 0.2f)
            {
                isSeek = false;
                lineRenderer.enabled = false;
            }
        }
       
    }
}
