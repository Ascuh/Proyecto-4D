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
    [SerializeField] float range;
    [SerializeField]List<Transform> targets = new List<Transform>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            Debug.Log("Adding Waypoint");
            AddWaypoint(player.transform.position); 
            timer = 0;
        }

        if (targets.Count > 0)
        {
            destPoint = targets[0].position;

            if (Vector3.Distance(transform.position, destPoint) < 20)
            {
                print("Moving to: " + destPoint);
                agent.SetDestination(destPoint);
                targets.RemoveAt(0);

                if (targets.Count > 0)
                {
                    destPoint = targets[0].position;
                }
            }
        }
    }

    public void AddWaypoint(Vector3 waypointPosition)
    {
        while (targets.Count > 0 && targets[0].position != waypointPosition)
        {
            targets.RemoveAt(0);
        }

        targets.Add(player.transform);
    }
}
