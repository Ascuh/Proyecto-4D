using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    [SerializeField] Camera cam1;
    [SerializeField] Camera cam2;
    [SerializeField] GameObject flashlight;

    [SerializeField] Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerMovement = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        cam2.enabled = false;
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
            cam2.enabled = true;
            cam1.enabled = false;
            anim.SetBool("kill", true);
            anim.SetBool("attack", false);
            agent.speed = 0;
            playerMovement.die();
            flashlight.SetActive(false);
            Invoke("MainMenu", 3);
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

    void MainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }


}
