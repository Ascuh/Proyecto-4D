using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Llave : MonoBehaviour
{
    MeshRenderer meshrenderer;

    public GameObject Llave2d;

    public static bool llave = false;

    Rigidbody rb;
    BoxCollider coll;
    public Transform player, ObjectContainer, MainCam;

    public float dropForwardForce, dropUpwardForce;

    bool equipped;
    public static bool slotFull;

    private void Start()
    {
     meshrenderer = GetComponent<MeshRenderer>();

        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();


        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;

        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        //se fija si el jugador esta en el rango y si la "e" esta apretada
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && RaycastCam.tocandoLlave && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        //si el arma esta equipada y se aprieta la "q"
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }
    public void Agarrollave()
    {
        llave = true;
        Llave2d.SetActive(true);
        meshrenderer.enabled = false;
    }

    public void Soltollave()
    {
        llave = false;
        Llave2d.SetActive(false);
        meshrenderer.enabled = true;
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //hace al objeto child de la camara y lo mueve a esa pocision
        transform.SetParent(ObjectContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        //hace al rigidbody a kinematic y el collider a trigger para que no se mueva más
        rb.isKinematic = true;
        coll.isTrigger = true;

        Agarrollave();

    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //hace que el objeto deje de ser child 
        transform.SetParent(null);

        //hace que el rigidbody no sea kinematic y el collider no sea trigger para que vuelva a la normalidad
        rb.isKinematic = false;
        coll.isTrigger = false;

        //el arma con las mismas fuerzas que el jugador 
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //da fuerza al arma para tirarla
        rb.AddForce(MainCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(MainCam.up * dropUpwardForce, ForceMode.Impulse);

        //agrega una rotacion random
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        Soltollave();
    }
}

   
