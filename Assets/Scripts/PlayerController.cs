using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    public LayerMask groundLayer;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody rb;
    public GameObject destinationPointPrefab;
    private GameObject destinationPoint;
    private Seeker seeker;
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        destinationPoint = Instantiate(destinationPointPrefab);
        destinationPoint.SetActive(false);
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.Play:

                if (Input.GetMouseButton(0))
                {    
                    RaycastHit hit; Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                    {
                        seeker.ShowSeeker(hit.point);
                        navMeshAgent.SetDestination(hit.point);
                        destinationPoint.transform.position = hit.point;
                        destinationPoint.SetActive(true);
                    }
                }

                if (navMeshAgent.velocity.magnitude > 0.2f && !anim.GetBool("Walk"))
                {
                    anim.SetBool("Walk", true);
                }
                else if (navMeshAgent.velocity.magnitude <= 0.2f && anim.GetBool("Walk"))
                {
                    destinationPoint.SetActive(false);
                    anim.SetBool("Walk", false);
                }
                break;


            case GameManager.GameState.Pause:
                
            break;
        }
        
    }
}
