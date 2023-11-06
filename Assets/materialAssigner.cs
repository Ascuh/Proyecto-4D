using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialAssigner : MonoBehaviour
{

    public Material referencedMaterial;
    public Terrain referencedTerrain;



    // Start is called before the first frame update
    void Start()
    {
        referencedTerrain.materialTemplate = referencedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
