using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image fade;
    private float fadeOut = 1.0f;
    private bool fading = false;

    public AudioSource audio;

    private int sceneToLoad = 0;

    private void Update()
    {
        if (!fading && fadeOut > 0)
        {
            fade.color = new Color(0, 0, 0, fadeOut);

            fadeOut -= Time.deltaTime;
        }
        else if (!fading && fadeOut <= 0)
            fade.enabled = false;
        else if (fading)
        {
            fade.enabled = true;

            fadeOut += Time.deltaTime;

            fade.color = new Color(0, 0, 0, fadeOut);

            if (fadeOut > 1)
                SceneManager.LoadScene(1);
        }

        audio.volume = 1 - fadeOut;
    }

    public void LoadScene(int scene)
    {
        sceneToLoad = scene;
        fading = true;
    }
}
