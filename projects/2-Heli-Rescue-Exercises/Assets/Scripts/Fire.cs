using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public List<ParticleSystem> FirePS = new List<ParticleSystem>();
    public float TimeToLiveInSec = 180f;
    public float StartIntensity = 0.5f;

    private void Awake()
    {
        foreach (var particleSystem in this.FirePS)
        {
            UpdateParticleAlphaTimeValue(particleSystem, 2, this.StartIntensity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseFireIntensityOverTime());
    }

    IEnumerator IncreaseFireIntensityOverTime()
    {
        var intensity = this.StartIntensity;

        while (intensity < 1.0f)
        {
            var intensityIncrement = (1 / TimeToLiveInSec);
            intensity += intensityIncrement;

            foreach (var particleSystem in this.FirePS)
            {
                UpdateParticleAlphaTimeValue(particleSystem, 2, intensity);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void UpdateParticleAlphaTimeValue(ParticleSystem particleSystem, int alphaKeyIndex, float newAlphaTime)
    {
        var colorModule = particleSystem.colorOverLifetime;
        var gradient = colorModule.color.gradient;
        var updatedGradient = new Gradient();

        GradientAlphaKey[] alphaKeys =
        {
            gradient.alphaKeys[0], gradient.alphaKeys[1],
            gradient.alphaKeys[2]
        };

        if (alphaKeyIndex > alphaKeys.Length)
        {
            throw new IndexOutOfRangeException("index is out of bounds");
        }

        alphaKeys[alphaKeyIndex].time = newAlphaTime;

        updatedGradient.SetKeys(gradient.colorKeys, alphaKeys);
        colorModule.color = updatedGradient;
    }
}