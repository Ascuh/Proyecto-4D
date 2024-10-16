using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monstruo_tres_cabezas : MonoBehaviour
{

    [SerializeField] Camera cam1;
    [SerializeField] Camera cam2;
    [SerializeField] Animator anim;
    public GameObject player;
    public GameObject jumpscareNoise;
    public GameObject blackScreen;
    public GameObject youdied;
    public GameObject monstruoMateriales;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject flashlightAnimation;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        cam2.enabled = false;
        anim = monstruoMateriales.GetComponent<Animator>();
        blackScreen.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("Stun", true);
        }
    }

    public void attackAnim ()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        anim.SetBool("Kill", true);
        jumpscareNoise.SetActive(true);
        StartCoroutine(activarPantallaNegra());
        playerMovement.die();
        flashlight.SetActive(false);
        flashlightAnimation.SetActive(true);
    }

    IEnumerator activarPantallaNegra()
    {
        yield return new WaitForSeconds(.65f);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        blackScreen.SetActive(false);
        youdied.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
