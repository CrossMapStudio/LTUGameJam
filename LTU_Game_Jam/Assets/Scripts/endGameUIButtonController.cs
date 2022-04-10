using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGameUIButtonController : MonoBehaviour
{
    private Animator anim;
    public string arg = "made";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void triggerDecision()
    {
        anim.SetTrigger(arg);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
