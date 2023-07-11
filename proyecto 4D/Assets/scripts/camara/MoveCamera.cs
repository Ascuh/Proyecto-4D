using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        //para que la camara se mueva con el personaje
        transform.position = cameraPosition.position;
    }
}
