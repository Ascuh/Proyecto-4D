using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderSpawner : MonoBehaviour
{
    public GameObject spiderPrefab; // El prefab de la ara�a
    public int maxSpiders = 10; // M�ximo n�mero de ara�as
    public float spawnInterval = 0.5f; // Tiempo entre cada spawn
    private int currentSpiderCount = 0; // Contador de ara�as
    private bool spawning = false; // Para evitar que se inicie m�s de una vez la rutina de spawn

    // Array de puntos de spawn
    public Transform[] spawnPoints;

    void Start()
    {
        // Verifica si hay puntos de spawn asignados
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No se han asignado puntos de spawn.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entr� al trigger tiene el tag "Player"
        if (other.CompareTag("Player") && !spawning)
        {
            // Inicia la rutina de spawn si el jugador entra
            StartCoroutine(SpawnSpider());
        }
    }

    IEnumerator SpawnSpider()
    {
        spawning = true; // Marca que el spawn ha comenzado
                         // Mientras no se haya alcanzado el m�ximo n�mero de ara�as
        while (currentSpiderCount < maxSpiders)
        {
            // Elige un spawn point aleatorio
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instancia una nueva ara�a
            GameObject newSpider = Instantiate(spiderPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

            // Aseg�rate de que la ara�a est� activa
            newSpider.SetActive(true);

            // Aumenta el contador de ara�as
            currentSpiderCount++;

            // Espera el intervalo antes de generar la siguiente ara�a
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
