using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class seeMonster : MonoBehaviour
{
    public Transform player;            // El jugador a seguir
    public float visionRadius;    // Distancia máxima de detección
    public float visionAngle;     // Ángulo del campo de visión
    public float attackDistance;   // Distancia mínima para atacar
    public Transform[] waypoints;       // Waypoints para deambular
    private NavMeshAgent agent;         // Agente NavMesh del monstruo
    private bool playerInSight = false; // Si el jugador está en el campo de visión
    private int currentWaypoint = 0;    // Índice del waypoint actual
    public monstruo_tres_cabezas Monstruo_Tres_Cabezas;
    public GameObject pasos;            // GameObject que representa el sonido de pasos
    private Vector3 lastPosition;       // Para almacenar la posición anterior del monstruo
    private bool isMoving = false;      // Si el monstruo se está moviendo

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
        lastPosition = transform.position; // Inicializa la posición inicial
    }

    void Update()
    {
        DetectPlayer();

        // Verificar si el monstruo está en movimiento
        isMoving = Vector3.Distance(lastPosition, transform.position) > 0.01f;

        if (isMoving)
        {
            pasos.SetActive(true); // Activa el objeto de sonido de pasos
        }
        else
        {
            pasos.SetActive(false); // Desactiva el objeto de sonido de pasos
        }

        lastPosition = transform.position; // Actualiza la última posición

        if (playerInSight)
        {
            // Si el jugador está en el campo de visión, seguirlo
            agent.SetDestination(player.position);

            // Verifica si el monstruo está lo suficientemente cerca como para atacar
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer <= attackDistance)
            {
                // Ejecuta la animación de ataque
                Monstruo_Tres_Cabezas.attackAnim();
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
}
