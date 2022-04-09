using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public CharacterController ctrl;
    public GameObject cam;
    public Camera mainCam;

    public float walkSpeed;
    public float lookSensitivity;

    private float lookElevation = 0;
    private float acceleration = -9.81f;
    private float fallSpeed = 0.0f;

    private bool talking;
    private Vector3 focalPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Movement axes
        // Horizontal movement
        float hor = Input.GetAxis("Horizontal") * walkSpeed;
        // Forward movement
        float ver = Input.GetAxis("Vertical") * walkSpeed;
        // Rotation along the horizon
        float azi = Input.GetAxis("Mouse X") * lookSensitivity;
        // Up and down rotation
        float ele = Input.GetAxis("Mouse Y") * lookSensitivity;

        // If player is not talking, they can move
        if (!talking)
        {
            // Increase fall speed if ungrounded
            if (ctrl.isGrounded)
                fallSpeed = 0.0f;
            else
                fallSpeed += acceleration * Time.deltaTime;

            // Look elevation is stored as a float due to the way Unity handles rotations (this is just easier)
            lookElevation = Mathf.Clamp(lookElevation -= ele, -89f, 89f);

            // Moves character in all directions
            ctrl.Move((transform.forward * ver + transform.right * hor + Vector3.up * fallSpeed) * Time.deltaTime);

            // Rotates camera
            cam.transform.localEulerAngles = new Vector3(lookElevation, 0f, 0f);

            // Rotates player's body side-to-side
            transform.eulerAngles += Vector3.up * azi;
        }
        // Player cannot move if they are talking
        else
        {
            cam.transform.LookAt(focalPoint);
        }
    }

    private void OnTriggerStay(Collider trigger)
    {
        // Talk with NPC if close enough and hitting interact key
        if (trigger.tag == "NPC" && Input.GetAxis("Interact") == 1)
        {
            focalPoint = trigger.transform.position;
            talking = true;
        }
    }
}
