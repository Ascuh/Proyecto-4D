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
    [SerializeField] float radius;
    [SerializeField] float angle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {

        if (!chasing)
        {
            Patrol();
        }



    }


    void Patrol()
    {
        if (!chasing)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attackPlayer();
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerLayer);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) > angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, obstacleLayer))
                    chasing = true;
                else chasing = false;
            }
            else chasing = false;
        }
    }
    private void attackPlayer()
    {

    }
}
