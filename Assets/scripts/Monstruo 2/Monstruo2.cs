    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    public class Monstruo2 : MonoBehaviour
    {
        bool walkPointSet;
        [SerializeField]bool patrol = true;

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
        private void FixedUpdate()
        {
            timer += Time.deltaTime;
            print(timer);

            if (timer >= 3)
            {
                Debug.Log("Adding Waypoint");
                AddWaypoint(player.transform.position);
                timer = 0;
            }
        }
        void Update()
        {


            if (patrol)
                Patrol();

            if (Vector3.Distance(transform.position, targets[0].position) < 5)
            {
                patrol = false;
            }


            if (!patrol)
                Chase();

        if (targets.Count > 5)
            targets.RemoveAt(0);
    }

        public void AddWaypoint(Vector3 waypointPosition)
        {

            targets.Add(player.transform);
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
            print("Moving to: " + destPoint);
            agent.SetDestination(destPoint);

            if (targets.Count > 0)
            {
                destPoint = targets[0].position;
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
