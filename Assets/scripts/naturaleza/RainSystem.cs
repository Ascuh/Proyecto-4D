 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour
{
    public ParticleSystem rain;
    float rate = 0;
    public float rateSum = 3;
    private AudioSource miAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        var rainEmission = rain.emission;
        miAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        startRain(5000f);
    }

    void startRain(float maxRain)
    {
        if (rate < maxRain)
        {
            rate += rateSum;
            miAudioSource.volume += 0.0007f;
        }

        miAudioSource.volume = Mathf.Clamp(miAudioSource.volume, 0f, 1f);
        var rainEmission = rain.emission;
        rainEmission.rateOverTime = rate;
    }
}
