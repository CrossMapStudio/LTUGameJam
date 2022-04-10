using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vCam;
    [SerializeField] private Transform lookAtTargetForDialogue;

    //Will need a check to see if the dialogue was already triggered before ->
    [SerializeField] private Vector3 offset, halfExtents;
    [SerializeField] private LayerMask playerLayer;
    private bool dialogueStarted = false;
    [SerializeField] private Dialogue dialogue_data;
    [SerializeField] private dialogueUIController dialogueUIElement;
    private dialogueUIController active;

    [SerializeField] private string subscriptionIDTriggerEvent;
    [SerializeField] private string actionTrackerID;

    private void Awake()
    {
        if (GameController.checkActionTracker(actionTrackerID))
        {
            enabled = false;
        }
    }

    private void FixedUpdate() {
    
        var col = Physics.OverlapBox(transform.position + offset, halfExtents, Quaternion.identity, playerLayer);
        if (col.Length > 0 && !dialogueStarted)
        {
            //Start Dialogue ->
            dialogueStarted = true;
            startDialogue();
        }
    }

    private void startDialogue()
    {
        GameController.holdPlayer = true;
        Cursor.lockState = CursorLockMode.Confined;
        active = Instantiate(dialogueUIElement, GameController.controller.currentCanvas.transform.position, Quaternion.identity, GameController.controller.currentCanvas.transform);
        active.end = new dialogueUIController.dialogueEnd(dialogueEnd);
        active.GetSetDialogueData = dialogue_data;
        vCam.LookAt = lookAtTargetForDialogue;
        active.setUI();
    }

    private void dialogueEnd()
    {
        GameController.holdPlayer = false;
        //Give control back to the player and camera -> Remove the box ->
        Destroy(active);
        Cursor.lockState = CursorLockMode.Locked;
        vCam.LookAt = null;

        if (subscriptionIDTriggerEvent != null)
        {
            GameController.callEvent(subscriptionIDTriggerEvent);
        }

        GameController.addActionToTracker(actionTrackerID);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + offset, halfExtents);
    }
}
