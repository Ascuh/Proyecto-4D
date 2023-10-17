 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour
{
    public ParticleSystem rain;
    float rate = 0;

    // Start is called before the first frame update
    void Start()
    {
        var rainEmission = rain.emission;
    }

    // Update is called once per frame
    void Update()
    {
        startRain(2000f);
    }

    void startRain(float maxRain)
    {
        if (rate < maxRain)
        {
            rate += 1;
        }

        var rainEmission = rain.emission;
        rainEmission.rateOverTime = rate;
    }
}
