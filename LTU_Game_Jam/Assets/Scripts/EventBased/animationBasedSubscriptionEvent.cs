using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class animationBasedSubscriptionEvent : MonoBehaviour
{
    [SerializeField] private string animationToPlay;
    private Animator anim;
    [SerializeField] private string ID;
    [SerializeField] private string invID;
    private Action action;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        action = new Action(triggerAnimation);
        GameController.addEvent(ID, action);

        if (invID != null)
        {
            GameController.callEventOnCheckInventory(invID, ID);
        }
    }

    private void triggerAnimation()
    {
        anim.Play(animationToPlay, 0, 0f);
    }
}
