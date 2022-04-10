using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Shake : MonoBehaviour
{
    // Shake values
    private float time = 0.0f;
    private float ogTime = 0.0f;
    private float amplitude = 0.0f;

    // Camera to shake
    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        if (noise == null)
            noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            // Applies amplitude
            noise.m_AmplitudeGain = amplitude * time / ogTime;

            Debug.Log("Amplitude: " + amplitude.ToString() + ", Time: " + time.ToString());

            // Decreases timer
            time -= Time.deltaTime;
        }
        // Once timer is out, reset values
        else if (time <= 0)
        {
            amplitude = 0;
            time = 0;
            noise.m_AmplitudeGain = 0;
            noise.m_FrequencyGain = 0;
        }
    }

    // Sets shake values
    public void CameraShake(float duration, float intensity, float frequency)
    {
        // Sets values
        time = duration;
        ogTime = duration;
        amplitude = intensity;

        // Applies frequency to camera shake
        noise.m_FrequencyGain = frequency;
    }
}
