using System;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;

public class NoiseExplosion: MonoBehaviour
{
    static public NoiseExplosion instance;
    [SerializeField] CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] AnimationCurve curveAmplitude;
    [SerializeField] float maxAmplitude;
    [SerializeField] AnimationCurve curveFrequency;
    [SerializeField] float maxFrequency;
    [SerializeField] float maxTime;
    float valueTime;
    public bool isNoise = false;
    public Vector3 targetPosition;
    [SerializeField] GameObject robot;
    [SerializeField] float maxDistance;
    [SerializeField] NoiseSettings noiseSettings;
    float valueCurveAmp;
    float valueCurveFre;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isNoise)
        {
            float distance = Vector3.Distance(targetPosition, robot.transform.position);
            if(distance/maxDistance <= 1)
            {
                noise.AmplitudeGain -= valueCurveAmp;
                noise.FrequencyGain -= valueCurveFre;

                valueTime += Time.deltaTime;

                valueTime = Math.Clamp(valueTime, 0, maxTime);

                valueCurveAmp = curveAmplitude.Evaluate(valueTime/maxTime) * maxAmplitude * distance/maxDistance;
                valueCurveFre = curveFrequency.Evaluate(valueTime/maxTime) * maxFrequency * distance/maxDistance;

                noise.NoiseProfile = noiseSettings;

                noise.AmplitudeGain += valueCurveAmp;
                noise.FrequencyGain += valueCurveFre;
            }
            if (valueTime >= maxTime)
            {
                isNoise = false;
            }

        }
        else
        {
            valueTime = 0;
            valueCurveAmp = 0;
            valueCurveFre = 0;
        }
    }
}
