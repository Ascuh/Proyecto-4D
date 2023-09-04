using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monstruo3 : MonoBehaviour
{
    float timer;
    [SerializeField] float range;

    bool walkPointSet;
    [SerializeField] bool patrol = true;

    Vector3 destPoint;    
    [SerializeField] List<Vector3> waypoints = new List<Vector3>();

    [SerializeField] LayerMask groundLayer;

    NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(patrol)
        Patrol();
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

    void AddWaypoint(Vector3 pos)
    {
        waypoints.Add(pos);
    }


}
