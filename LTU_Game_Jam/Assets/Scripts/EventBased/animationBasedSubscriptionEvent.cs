using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class animationBasedSubscriptionEvent : MonoBehaviour
{
    [SerializeField] private string animationToPlay;
    private Animator anim;
    [SerializeField] private string ID;
    private Action action;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        action = new Action(triggerAnimation);
        GameController.addEvent(ID, action);
    }

    private void triggerAnimation()
    {
        anim.Play(animationToPlay, 0, 0f);
    }
}
