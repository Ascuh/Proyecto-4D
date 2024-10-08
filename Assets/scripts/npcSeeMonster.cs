using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcSeeMonster : MonoBehaviour
{
    public Transform player;            // El jugador a seguir
    public float visionRadius = 10f;    // Distancia m�xima de detecci�n
    public float visionAngle = 45f;     // �ngulo del campo de visi�n
    public float attackDistance = 2f;   // Distancia m�nima para atacar
    private NavMeshAgent agent;         // Agente NavMesh del monstruo
    private bool playerInSight = false; // Si el jugador est� en el campo de visi�n
    private bool hasSeenPlayer = false; // Si el monstruo ha visto al jugador alguna vez
    public monstruo_tres_cabezas Monstruo_Tres_Cabezas; // Clase que controla la animaci�n de ataque del monstruo
    public GameObject pasos; // Efecto de sonido de pasos

    void Start()
    {
        // Asigna autom�ticamente el player con el tag "Player"
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("No se encontr� un objeto con el tag 'Player'.");
            }
        }

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        DetectPlayer();

        // Si el monstruo ha visto al jugador una vez, lo seguir� para siempre
        if (hasSeenPlayer)
        {
            pasos.SetActive(true);  // Activa el sonido de pasos

            // Seguir al jugador
            agent.SetDestination(player.position);

            // Verifica si el monstruo est� lo suficientemente cerca como para atacar
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer <= attackDistance)
            {
                // Ejecuta la animaci�n de ataque
                Monstruo_Tres_Cabezas.attackAnim();
            }
        }
        else
        {
            // Si el jugador no est� a la vista, el monstruo se queda quieto
            agent.SetDestination(transform.position);
        }
    }

    void DetectPlayer()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Verifica si el jugador est� dentro del rango de visi�n y �ngulo
        if (distanceToPlayer <= visionRadius)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= visionAngle)
            {
                // Dibuja el Raycast en la escena para ver la l�nea de visi�n
                Debug.DrawRay(transform.position, directionToPlayer * visionRadius, Color.red);

                // Realiza un raycast para asegurarse de que no haya obst�culos entre el monstruo y el jugador
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionRadius))
                {
                    if (hit.transform == player)
                    {
                        playerInSight = true;
                        hasSeenPlayer = true; // El monstruo ahora siempre te seguir�
                        return;
                    }
                }
            }
        }

        playerInSight = false;
    }
}
