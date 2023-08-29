    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    public class Monstruo2 : MonoBehaviour
    {
        bool walkPointSet;
        [SerializeField]bool patrol = true;

        float closestDistance;
        int closestIndex = 0;
        [HideInInspector] public bool deny;

        public GameObject player;
        NavMeshAgent agent;
        [SerializeField] LayerMask groundLayer, playerLayer, obstacleLayer;

        float timer;
        Vector3 destPoint;
        [SerializeField] float range;
        [SerializeField]List<Vector3> targets = new List<Vector3>();

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        private void FixedUpdate()
        {
            timer += Time.deltaTime;
            print(timer);

            if (timer >= 3 && !deny)
            {
                Debug.Log("Adding Waypoint");
                Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                AddWaypoint(playerPos);
                timer = 0;
            }
        }
        void Update()
        {
                if (patrol)
                Patrol();

        if (targets.Count > 0)
        {
            closestDistance = Vector3.Distance(transform.position, targets[0]);

            for (int i = 1; i < targets.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, targets[i]);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            if (closestDistance < 5)
            {
                patrol = false;
            }
        }


            if (!patrol)
                Chase(closestIndex);

    }

        public void AddWaypoint(Vector3 waypointPosition)
        {

            targets.Add(waypointPosition);
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

    void Chase(int targetIndex)
    {
        if (targets.Count > 0 && targetIndex >= 0 && targetIndex < targets.Count)
        {
            if(Vector3.Distance(transform.position, targets[targetIndex]) > 20)
            {
                targets.RemoveAt(targetIndex);   
            }
            Vector3 destPoint = targets[targetIndex];
            agent.SetDestination(destPoint);
            print("Moving to: " + destPoint);

            if (Vector3.Distance(transform.position, destPoint) < 1.0f)
            {
                targets.RemoveAt(targetIndex);
            }
        }
        else
        {
            patrol = true;
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

    }
