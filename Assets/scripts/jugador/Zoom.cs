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

    private void Start()
    {
        playerCam = FindObjectOfType<PlayerCam>();
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
            Camera.main.transform.LookAt(target);
            orientation.transform.LookAt(target);
            zoomIn();
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
        if(Camera.main.fieldOfView > minFOV)
            Camera.main.fieldOfView--;
        if (Camera.main.fieldOfView <= minFOV)
            Invoke("xFalse", 1);
    }

    private void zoomOut()
    {
        if (Camera.main.fieldOfView < maxFOV)
            Camera.main.fieldOfView++;
    }

    private void xFalse()
    {
        x = false;
    }
}
