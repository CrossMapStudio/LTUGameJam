using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Play : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource player;
    public bool soundPlay;

    public void PlaySound()
    {
        if (soundPlay)
            player.PlayOneShot(sound);
    }
}
