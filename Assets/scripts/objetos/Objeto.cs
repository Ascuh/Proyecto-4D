using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objeto : MonoBehaviour
{
    public static bool drop = false;
    MeshRenderer meshrenderer;


    // Start is called before the first frame update
    void Start()
    {
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
            Destroy(gameObject);
        }
    }

    public void Agarro()
    {
        meshrenderer.enabled = false;
    }

    public void Solto()
    {
        meshrenderer.enabled = true;
    }



}
