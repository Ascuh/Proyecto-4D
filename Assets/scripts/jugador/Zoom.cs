using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject orientation;
    [SerializeField] bool x;
    [SerializeField] float dist;
    [SerializeField] float minFOV;
    [SerializeField] float maxFOV;
    bool alreadyZoomed;
    PlayerCam playerCam;
    Camera cam;  // Referencia a la c�mara

    private void Start()
    {
        playerCam = FindObjectOfType<PlayerCam>();
        cam = Camera.main;  // Guarda la c�mara principal
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < dist && !alreadyZoomed)
        {
            x = true;
        }

        if (x)
        {
            playerCam.zoom = true;
            if (cam != null)  // Aseg�rate de que cam no sea nulo
            {
                cam.transform.LookAt(target);
                zoomIn();
            }

            if (orientation != null)  // Verifica que orientation no sea nulo
            {
                orientation.transform.LookAt(target);
            }

            alreadyZoomed = true;
        }
        else
        {
            zoomOut();
            playerCam.zoom = false;
        }
    }

    private void zoomIn()
    {
        if (cam != null && cam.fieldOfView > minFOV)
            cam.fieldOfView--;

        if (cam != null && cam.fieldOfView <= minFOV)
            Invoke("xFalse", 1);
    }

    private void zoomOut()
    {
        if (cam != null && cam.fieldOfView < maxFOV)
            cam.fieldOfView++;
    }

    public void xFalse()  // Hacer p�blico para que Invoke funcione
    {
        x = false;
    }
}
