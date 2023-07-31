using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monstruo1 : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    [SerializeField]LayerMask groundLayer, playerLayer, obstacleLayer;
    [SerializeField]bool chasing = false;

    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float range;
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    bool inSight = false;
    bool inRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        inSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        inRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!inSight && !inRange)
            Patrol();
        
        if (inSight || !inRange)
            Chase();

        if (inSight && inRange)
            Attack();

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

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {

    }

    void SearchForDest()
    {
        if (!chasing)
        {
        float z = Random.Range(range, -range);
        float x = Random.Range(range, -range);
        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
        }

    }

}
