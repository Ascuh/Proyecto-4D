using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{

    //WORKS WITH ALL THE NATURE/TREE CREATOR SHADERS

    private Renderer render;
    private Material[] mats;
    /*
     * The Vector4 wind value works as :
     *  -> x = wind offset on the x value of the leaves
     *  -> y = *same for y
     *  -> z = *same for z
     *  -> w = Wind force applied to all the values above
    */
    [Header("Set the length of the array the same as the number of materials")]
    [Tooltip("x, y, z -> wind offset; w -> wind force")]
    [SerializeField] private Vector4[] Wind;
    /*
     * Good settings to start with :
     * Leafs : Vector4(0.1f, 0.1f, 0.1f, 0.1f)
     * Trunk : Vector4(0, 0, 0, -0.03f)
     * 
     * Put smaller values if your tree isn't vertex paint
     * 
     * Set a negative value to the trunk, makes it moving with the leaves (leaves must have a positive value)
     * 
     * This script looks very weird and bad with strong wind forces
    */

    void Awake()
    {
        if (!render) render = GetComponent<Renderer>();
        mats = render.materials;
    }

    void Update()
    {
        //This script will affects each material with one Vector4 in the same order
        for (int i = 0; i < mats.Length; i++)
            mats[i].SetVector("_Wind", Wind[i]);
    }
}
