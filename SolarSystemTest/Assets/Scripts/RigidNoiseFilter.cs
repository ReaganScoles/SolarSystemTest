using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : iNoiseFilter
{
    NoiseSettings.RigidNoiseSettings settings;
    Noise noise = new Noise();

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        //float noiseValue = (noise.Evaluate(point * settings.roughness + settings.center) + 1) * 0.5f;
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1.0f;
        float weight = 1.0f;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.center));   //Inverted absolute value
            v *= v; //v^2
            //noiseValue += (v + 1) * 0.5f * amplitude;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier); //Doing this makes regions that start relatively low-down are undetailed compared to regions that start high-up

            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);

        return noiseValue * settings.strength;
    }
}
