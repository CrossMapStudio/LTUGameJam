using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darken_Fog : MonoBehaviour
{
    private bool darken = false;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == "Player")
            darken = true;
    }

    private void Update()
    {
        if (darken)
        {
            // Darkens color by lowering rgb values of fog
            float r = Mathf.Clamp(RenderSettings.fogColor.r - (Time.deltaTime * 100), 0, Mathf.Infinity);
            float g = Mathf.Clamp(RenderSettings.fogColor.g - (Time.deltaTime * 100), 0, Mathf.Infinity);
            float b = Mathf.Clamp(RenderSettings.fogColor.b - (Time.deltaTime * 100), 0, Mathf.Infinity);

            // Increases density of fog to accentuate the effect
            RenderSettings.fogDensity = RenderSettings.fogDensity + Time.deltaTime * 0.1f;

            // Applies colors to fog
            RenderSettings.fogColor = new Color(r, g, b);
        }
    }
}
