using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeManager : MonoBehaviour
{

    float alphaCutoff;
    // Start is called before the first frame update
    void Start()
    {
        alphaCutoff = Random.Range(0.35f, 0.65f);
        GetComponent<MeshRenderer>().materials[1].SetFloat("_AlphaCutoff", alphaCutoff);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
