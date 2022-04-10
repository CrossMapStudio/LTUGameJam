using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fade;
    private float fadeOut = 1.0f;
    public float multiplier;

    private void Awake()
    {
        if (multiplier == 0)
            multiplier = 1;

        fade.enabled = true;
    }

    void Update()
    {
        if (fadeOut > 0)
        {
            fade.enabled = true;
            fadeOut -= Time.deltaTime * multiplier;
            fade.color = new Color(0, 0, 0, fadeOut);
        }
        else
            fade.enabled = false;
    }
}
