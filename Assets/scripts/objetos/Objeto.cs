using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objeto : MonoBehaviour
{
    public static bool drop = false;
    MeshRenderer meshrenderer;
    public GameObject objeto2d;
    Monstruo3 monstruo3;

    // Start is called before the first frame update
    void Start()
    {
        monstruo3 = FindObjectOfType<Monstruo3>();
        meshrenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && drop)
        {
            monstruo3.AddWaypoint(transform.position);
            Destroy(gameObject);
        }
    }

    public void Agarro()
    {
        objeto2d.SetActive(true);
        meshrenderer.enabled = false;
    }

    public void Solto()
    {
        objeto2d.SetActive(false);
        meshrenderer.enabled = true;
    }



}
