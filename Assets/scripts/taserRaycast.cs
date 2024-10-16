using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taserRaycast : MonoBehaviour
{
    // Distancia máxima del raycast
    public float distanciaRaycast = 100f;
    [SerializeField] int tiempoStun;

    // Cámara específica desde la que lanzar el raycast
    public Camera camaraEspecifica;

    public void DetectarMonstruo()
    {

        // Verificar que la cámara específica esté asignada
        if (camaraEspecifica == null)
        {
            Debug.LogError("No se ha asignado una cámara específica.");
            return;
        }

        // Crear un ray desde la cámara específica en la dirección del mouse
        Ray ray = camaraEspecifica.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Visualizar el raycast en la ventana de escena
        Debug.DrawRay(ray.origin, ray.direction * distanciaRaycast, Color.red, 1f);

        // Realizar el raycast
        if (Physics.Raycast(ray, out hit, distanciaRaycast))
        {

            // Verificar si el objeto con el que impacta el raycast tiene el tag "monstruo"
            if (hit.collider.CompareTag("monstruo"))
            {
                // Obtener el componente seeMonster del objeto impactado
                seeMonster SeeMonster = hit.collider.GetComponent<seeMonster>();

                // Si el monstruo tiene el componente seeMonster
                if (SeeMonster != null)
                {
                    // Iniciar el stun
                    StartCoroutine(stun(SeeMonster));
                }

                else
                {
                    // Obtener el componente seeMonster del objeto impactado
                    npcSeeMonster NpcSeeMonster = hit.collider.GetComponent<npcSeeMonster>();

                    // Si el monstruo tiene el componente seeMonster
                    if (NpcSeeMonster != null)
                    {
                        // Iniciar el stun
                        StartCoroutine(stunNpc(NpcSeeMonster));
                    }
                }
            }
        }
    }

    // Modificación: pasamos el SeeMonster específico como parámetro
    IEnumerator stun(seeMonster SeeMonster)
    {
        SeeMonster.StopMovement();  // Detiene el monstruo
        yield return new WaitForSeconds(tiempoStun);  // Esperar ? segundos
        SeeMonster.StartMovement();  // Reactiva el movimiento del monstruo
    }

    IEnumerator stunNpc(npcSeeMonster NpcSeeMonster)
    {
        NpcSeeMonster.StopMovement();  // Detiene el monstruo
        yield return new WaitForSeconds(tiempoStun);  // Esperar ? segundos
        NpcSeeMonster.StartMovement();  // Reactiva el movimiento del monstruo
    }
}
