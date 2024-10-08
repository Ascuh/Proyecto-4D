using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class seeMonster : MonoBehaviour
{
    public Transform player;            // El jugador a seguir
    public float visionRadius = 10f;    // Distancia máxima de detección
    public float visionAngle = 45f;     // Ángulo del campo de visión
    public float attackDistance = 2f;   // Distancia mínima para atacar
    public Transform[] waypoints;       // Waypoints para deambular
    private NavMeshAgent agent;         // Agente NavMesh del monstruo
    private bool playerInSight = false; // Si el jugador está en el campo de visión
    private int currentWaypoint = 0;    // Índice del waypoint actual

    void Start()
    {
        // Asigna automáticamente el player con el tag "Player"
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("No se encontró un objeto con el tag 'Player'.");
            }
        }

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        DetectPlayer();

        if (playerInSight)
        {
            // Si el jugador está en el campo de visión, seguirlo
            agent.SetDestination(player.position);

            // Verifica si el monstruo está lo suficientemente cerca como para atacar
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer <= attackDistance)
            {
                // Ejecuta la animación de ataque
                attackAnim();
            }
        }
        else
        {
            Patrol();
        }
    }

    void DetectPlayer()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Verifica si el jugador está dentro del rango de visión y ángulo
        if (distanceToPlayer <= visionRadius)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= visionAngle)
            {
                // Dibuja el Raycast en la escena para ver la línea de visión
                Debug.DrawRay(transform.position, directionToPlayer * visionRadius, Color.red);

                // Realiza un raycast para asegurarse de que no haya obstáculos entre el monstruo y el jugador
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionRadius))
                {
                    if (hit.transform == player)
                    {
                        playerInSight = true;
                        return;
                    }
                }
            }
        }

        playerInSight = false;
    }

    void Patrol()
    {
        // Si el monstruo ha llegado al waypoint actual, ir al siguiente
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    // Método para ejecutar la animación de ataque
    void attackAnim()
    {
        // Aquí puedes poner la lógica para la animación de ataque
        Debug.Log("Ataque ejecutado!");
    }
}
