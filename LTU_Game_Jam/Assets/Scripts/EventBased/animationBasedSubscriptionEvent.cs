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
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (invID == "")
        {
            action = new Action(triggerAnimation);
            GameController.addEvent(ID, action);
        }
        else
        {
            action = new Action(triggerAnimation);
            GameController.addEventAfterInvCheck(invID, ID, action);
            GameController.callEventOnCheckInventory(invID, ID);
        }

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
