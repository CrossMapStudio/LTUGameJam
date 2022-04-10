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
        Cursor.lockState = CursorLockMode.Confined;
        active = Instantiate(dialogueUIElement, GameController.controller.currentCanvas.transform.position, Quaternion.identity, GameController.controller.currentCanvas.transform);
        active.end = new dialogueUIController.dialogueEnd(dialogueEnd);
        active.GetSetDialogueData = dialogue_data;
        vCam.LookAt = lookAtTargetForDialogue;
        active.setUI();
    }

    private void dialogueEnd()
    {
        //Give control back to the player and camera -> Remove the box ->
        Destroy(active);
        vCam.LookAt = null;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + offset, halfExtents);
    }
}
