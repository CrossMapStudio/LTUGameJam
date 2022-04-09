using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_And_Tooltip : MonoBehaviour
{
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    /*~~~~~~~~~~~~~~~~~~~~~ NPC DIALOGUE PROMPT ~~~~~~~~~~~~~~~~~~~~~~~~~*/
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    // Camera stuff for raycasts
    public GameObject cam;
    public Camera mainCam;

    public bool talking;

    // Raycast LayerMask to ignore player
    public LayerMask ignorePlayer;

    // UI Elements
    public Text toolTip;
    public GameObject dialogueBox;
    // Displays name of person speaking
    public Text speakerText;
    // Displays dialogue
    public Text dialogueText;
    // Shows 'Hit 'E' to continue' prompt while talking to an NPC
    public Text continueText;

    // toolTip opacity
    private float textOpacity = 0.0f;
    private bool opacityUp = false;

    // Dialogue
    private Dialogue dialogue;
    private int currentDialogue = 0;


    // Update is called once per frame
    void Update()
    {
        if (!talking)
        {
            // Raycast to detect NPC with dialogue
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;

            // Detects if an object is hit
            if (Physics.Raycast(ray, out hit, 2.5f, ignorePlayer))
            {
                // NPC is detected
                if (hit.collider.tag == "NPC")
                {
                    toolTip.enabled = true;
                    TextOpacityChange(toolTip);

                    toolTip.text = "Hit 'E' to talk with " + hit.collider.name;

                    // Initiate dialogue with NPC
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        InitializeDialogue(hit.collider.gameObject);
                        GetComponent<Player_Move>().talking = true;
                        ResetTooltipText();
                    }
                }
                // Disables tooltip text if something other than NPC is hit
                else
                {
                    ResetTooltipText();
                }
            }
            // Reset tooltip text if no collider is hit
            else if (hit.collider == null)
            {
                ResetTooltipText();
            }
            // Disable text box if it is still active
            if (dialogueBox.activeInHierarchy)
                dialogueBox.SetActive(false);
        }
        // Player cannot move if they are talking
        else
        {
            // Remove tooltip while talking
            if (toolTip.enabled)
                ResetTooltipText();

            // Makes player's camera focus on person speaking (NOT IN USE RIGHT NOW)
            //   cam.transform.LookAt(focalPoint);

            // Enable text box if it is not active
            if (!dialogueBox.activeInHierarchy)
                dialogueBox.SetActive(true);

            TextOpacityChange(continueText);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if ((currentDialogue + 1) < dialogue.Text.Length)
                {
                    currentDialogue++;
                    NextDialogue(currentDialogue);
                }
                else
                {
                    talking = false;
                    dialogueBox.SetActive(false);
                    GetComponent<Player_Move>().talking = false;
                }
            }
        }
    }



    // Adds pulsing effect to text
    private void TextOpacityChange(Text textBox)
    {
        if (!opacityUp)
        {
            if (textOpacity < 1)
                textOpacity += Time.deltaTime * 0.66f;
            else
                opacityUp = true;
        }
        else
        {
            if (textOpacity > 0.5f)
                textOpacity -= Time.deltaTime * 0.33f;
            else
                opacityUp = false;
        }

        textBox.color = new Color(toolTip.color.r, toolTip.color.g, toolTip.color.b, textOpacity);
    }
    // Resets toolTip text
    private void ResetTooltipText()
    {
        toolTip.enabled = false;
        ResetPulsingText();
    }
    // Resets pulsing text properties
    private void ResetPulsingText()
    {
        textOpacity = 0f;
        opacityUp = false;
    }

    private void InitializeDialogue(GameObject npc)
    {
        dialogue = npc.GetComponent<NPC_Dialogue>().dialogue;
        talking = true;
        currentDialogue = 0;
        dialogueBox.SetActive(true);

        speakerText.text = npc.name;
        dialogueText.text = dialogue.Text[0];
    }

    public void NextDialogue(int index)
    {
        dialogueText.text = dialogue.Text[index];
    }
}
