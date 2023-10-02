    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

public class Monstruo3 : MonoBehaviour
{
    bool walkPointSet;
    [SerializeField] bool patrol = true;

    public GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer, obstacleLayer;

    float timer;
    Vector3 destPoint;
    [SerializeField] float range;
    [SerializeField] List<Vector3> targets = new List<Vector3>();
    private bool chaseObj;
    private bool chasePlyr;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {

    }
    void Update()
    {
        if (patrol)
            Patrol();
        else if (chaseObj)
            ChaseObject();
        else if (chasePlyr)
            ChasePlayer();
    }



    public void AddWaypoint(Vector3 waypointPosition)
    {
        targets.Add(waypointPosition);
        agent.SetDestination(targets[0]);
        chaseObj = true;
    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            SearchForDest();
        }

        if (walkPointSet)
        {
            agent.SetDestination(destPoint);
        }

        if (Vector3.Distance(transform.position, destPoint) < 10)
        {
            walkPointSet = false;
        }
    }



    void SearchForDest()
    {
        float z = Random.Range(range, -range);
        float x = Random.Range(range, -range);
        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }
    void ChaseObject()
    {
        if(agent.remainingDistance < 1)
        {
            targets.RemoveAt(0);
            agent.SetDestination(targets[0]);
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    }
