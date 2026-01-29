using System;
using Unity.Cinemachine;
using UnityEngine;

public class NoiseShoot : MonoBehaviour
{
    static public NoiseShoot instance;
    [SerializeField] CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] AnimationCurve curveAmplitude;
    [SerializeField] float maxAmplitude;
    [SerializeField] AnimationCurve curveFrequency;
    [SerializeField] float maxFrequency;
    [SerializeField] float maxTime;
    float valueTime;
    public bool isNoise = false;
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
            noise.AmplitudeGain -= valueCurveAmp;
            noise.FrequencyGain -= valueCurveFre;

            valueTime += Time.deltaTime;

            valueTime = Math.Clamp(valueTime, 0, maxTime);

            valueCurveAmp = curveAmplitude.Evaluate(valueTime/maxTime) * maxAmplitude ;
            valueCurveFre = curveFrequency.Evaluate(valueTime/maxTime) * maxFrequency ;

            noise.NoiseProfile = noiseSettings;

            noise.AmplitudeGain += valueCurveAmp;
            noise.FrequencyGain += valueCurveFre;

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
