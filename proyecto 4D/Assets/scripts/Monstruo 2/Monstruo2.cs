using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monstruo2 : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer, obstacleLayer;

    float timer;

    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float range;
    List<Transform> targets;

    void Start()
    {
        destPoint = targets[0].position;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1)
            AddWaypoint();


        if (Vector3.Distance(transform.position, destPoint) < 10)
        {
            targets.RemoveAt(0);
            destPoint = targets[0].position;
        }
    }



    public void AddWaypoint()
    {
        targets.Add(player.transform);
    }

}
