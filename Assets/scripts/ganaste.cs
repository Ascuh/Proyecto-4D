using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ganaste : MonoBehaviour
{
    // Arrastra el GameObject que quieres activar desde el editor de Unity
    public GameObject textoGanaste;
    public GameObject sonidoGanaste;

    // Esta función se ejecuta cuando otro collider entra en contacto con este objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que tocó tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Llama a la función para activar el GameObject
            Ganaste();
        }
    }

    // Función que activa el GameObject
    private void Ganaste()
    {
        if (textoGanaste != null)
        {
            textoGanaste.SetActive(true);
            sonidoGanaste.SetActive(true);
        }
    }
}
