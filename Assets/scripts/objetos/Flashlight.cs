using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject lightDetect;
    public float battery = 100f;
    public int batteryInt = 100;
    public GameObject[] batteryIcons;
    bool on;
    private Coroutine parpadeo1;
    private Coroutine parpadeo2;
    bool corroutineOn = true;
    int index = 1;
    int index2 = 1;
    bool isBattery = true;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
        flashlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        batteryInt = Mathf.RoundToInt(battery);
        Debug.Log(batteryInt);
        Debug.Log(on);
        if (on)
        {
            linternaBateria();
        }

        //Checkeando condiciones para que se prenda la flashlight

        {
            if (!on && Input.GetKeyDown(KeyCode.F) && isBattery)
            {
                flashlight.SetActive(true);
                on = true;


                RaycastHit HitInfo;
                if (Physics.Raycast(flashlight.transform.position, flashlight.transform.forward, out HitInfo))
                {
                    lightDetect.transform.position = (HitInfo.point);
                }
            }
            else if (on && Input.GetKeyDown(KeyCode.F))
            {
                flashlight.SetActive(false);
                on = false;
                if (parpadeo1 != null && parpadeo2 != null)
                {
                    StopAllCoroutines();
                }
                corroutineOn = false;
            }
        }

        if (!on && batteryInt < 101 && isBattery)
        {
            battery += 5 * Time.deltaTime;
            index = 1;
            index2 = 1;

            if (batteryInt > -1 && batteryInt < 25)
            {
                batteryIcons[4].SetActive(false);
                batteryIcons[3].SetActive(true);
            }
            else if (batteryInt >= 25 && batteryInt < 50)
            {
                batteryIcons[3].SetActive(false);
                batteryIcons[2].SetActive(true);
            }
            else if (batteryInt >= 50 && batteryInt < 75)
            {
                batteryIcons[2].SetActive(false);
                batteryIcons[1].SetActive(true);
            }
            else
            {
                batteryIcons[1].SetActive(false);
                batteryIcons[0].SetActive(true);
            }
        }

        IEnumerator parpadeo(float delay)
        {
            while (corroutineOn)
            {
                yield return new WaitForSeconds(delay);
                flashlight.SetActive(false);
                yield return new WaitForSeconds(delay);
                flashlight.SetActive(true);
            }
        }
        void linternaBateria()
        {
            if (batteryInt > -1)
            {
                battery -= 5 * Time.deltaTime;

                if (batteryInt <= 75 && batteryInt > 50)
                {
                    batteryIcons[0].SetActive(false);
                    batteryIcons[1].SetActive(true);
                }
                else if (batteryInt <= 50 && batteryInt > 25)
                {
                    corroutineOn = true;
                    batteryIcons[1].SetActive(false);
                    batteryIcons[2].SetActive(true);
                    while (index > 0)
                    {
                        parpadeo1 = StartCoroutine(parpadeo(.5f));
                        index -= 1;
                    }
                }
                else if (batteryInt <= 25 && batteryInt > 0)
                {
                    if (parpadeo1 != null)
                    {
                        StopCoroutine(parpadeo1);
                    }
                    corroutineOn = false;
                    batteryIcons[2].SetActive(false);
                    batteryIcons[3].SetActive(true);
                    corroutineOn = true;
                    while (index2 > 0)
                    {
                        parpadeo2 = StartCoroutine(parpadeo(.25f));
                        index2 -= 1;
                    }
                   //arpadeo1 = null;
                }
                else if (batteryInt < 0)
                {
                     StopCoroutine(parpadeo2);
                    corroutineOn = false;
                    batteryIcons[3].SetActive(false);
                    batteryIcons[4].SetActive(true);
                    flashlight.SetActive(false);
                    StopAllCoroutines();
                    isBattery = false;
                    on = false;


                }
            }
        }
    }
}
