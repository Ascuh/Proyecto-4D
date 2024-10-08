using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monstruo_tres_cabezas : MonoBehaviour
{

    [SerializeField] Camera cam1;
    [SerializeField] Camera cam2;
    [SerializeField] Animator anim;
    public GameObject player;
    public GameObject blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        cam2.enabled = false;
        anim = GetComponent<Animator>();
        blackScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            attackAnim();
        }
    }

    void attackAnim ()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        anim.SetBool("Kill", true);
        StartCoroutine(activarPantallaNegra());
    }

    IEnumerator activarPantallaNegra()
    {
        yield return new WaitForSeconds(.65f);
        blackScreen.SetActive(true);
    } 
}
