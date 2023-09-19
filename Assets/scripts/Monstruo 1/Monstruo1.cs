using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monstruo1 : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    [SerializeField]LayerMask groundLayer, playerLayer, obstacleLayer;
    PlayerMovement playerMovement;


    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float range;
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    [SerializeField] bool inSight = false;
    [SerializeField] bool inRange = false;

    [SerializeField ]Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerMovement = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
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
        if (inSight)
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        if (playerMovement.life)
        {
            anim.SetBool("attack", true);
            anim.SetBool("kill", false);
            agent.speed = 0;
            Invoke("resetSpeed", 3);
        }
         else
        {
            anim.SetBool("kill", true);
            anim.SetBool("attack", false);
            agent.speed = 0;
            playerMovement.die();
            Invoke("resetSpeed", 3);
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

    void resetSpeed()
    {
        agent.speed = 3.5f;
        playerMovement.life = false;
        anim.SetBool("attack", false);
        anim.SetBool("kill", false);
    }

}
