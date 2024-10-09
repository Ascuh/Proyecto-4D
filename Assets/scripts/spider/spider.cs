using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spider : MonoBehaviour
{


    private NavMeshAgent agent;  // Referencia al NavMeshAgent
    private Transform player;     // Referencia al transform del jugador
    spiderManager SpiderManager;
    PlayerMovement playerMovement;

    void Start()
    {
        // Obtiene el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Busca al jugador por su tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Obtiene el componente SpiderManager en el mismo objeto o en el padre
        SpiderManager = FindObjectOfType<spiderManager>();
        if (SpiderManager == null)
        {
            Debug.LogError("No se encontr� el componente SpiderManager en este objeto.");
        }

        playerMovement = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (player != null)
        {
            // Mueve a la ara�a hacia el jugador
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si colisiona con el jugador
        if (other.CompareTag("Player"))
        {
            LaAra�aTeAtaco();
            // Destruye la ara�a
            Destroy(gameObject);
        }
    }

    private void LaAra�aTeAtaco()
    {
        SpiderManager.ara�asPegadas++;
    }
}
