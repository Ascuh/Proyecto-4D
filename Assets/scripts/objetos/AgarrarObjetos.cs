using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AgarrarObjetos : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider coll;
    public Transform player, ObjectContainer, MainCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    public Objeto objeto;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    { 

        //se fija si el jugador esta en el rango y si la "e" esta apretada
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && RaycastCam.tocandoObj && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        //si el arma esta equipada y se aprieta la "q"
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //hace al objeto child de la camara y lo mueve a esa pocision
        transform.SetParent(ObjectContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //hace al rigidbody a kinematic y el collider a trigger para que no se mueva más
        rb.isKinematic = true;
        coll.isTrigger = true;

        objeto.Agarro();

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

        Objeto.drop = true;

        objeto.Solto();
    }



}
