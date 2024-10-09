using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectarAlejandose : MonoBehaviour
{
    // El GameObject que se activar�
    public GameObject textoAlejandose;

    // El tiempo que el GameObject permanecer� activado
    public float activeTime = 5f;

    // Chequea si el objeto a colisionar es el jugador
    private void OnTriggerEnter(Collider other)
    {
        // Cambia "Player" por el nombre del tag que tenga tu jugador
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateObject());
        }
    }

    // Corutina para activar el objeto y desactivarlo despu�s del tiempo especificado
    private IEnumerator ActivateObject()
    {
        textoAlejandose.SetActive(true);  // Activar el objeto
        yield return new WaitForSeconds(activeTime);  // Esperar el tiempo deseado
        textoAlejandose.SetActive(false);  // Desactivar el objeto
    }
}
