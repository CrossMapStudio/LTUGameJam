using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    /*~~~~~~~~~~~~~~~~~~~~~~~ PLAYER MOVEMENT ~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    // Main components
    public CharacterController ctrl;
    public GameObject cam;
    public Camera mainCam;

    // Movement modifiers
    public float walkSpeed;
    public float lookSensitivity;

    // Movement values
    private float lookElevation = 0;
    private float acceleration = -9.81f;
    private float fallSpeed = 0.0f;

    // Number to multiply acceleration due to gravity by
    public float gravityMultiplier;

    // Stops player from moving or interacting further when talking with an NPC
    public bool talking;

    // The position that the camera focuses on when talking to an NPC (CURRENTLY UNUSED)
    private Vector3 focalPoint = Vector3.zero;

    // Layermask for raycast (ignores the player)
    public LayerMask ignorePlayer;

    // Footstep sound player
    public AudioSource aSource;
    public AudioClip step;
    public float stepTime;
    private bool stepping = false;
    // Ensures sound won't play if player is still
    private float vel;

    // Start is called before the first frame update
    void Awake()
    {
        // Locks Cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Finds camera
        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCam == null)
            mainCam = cam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement axes
        // Horizontal movement
        float hor = Input.GetAxis("Horizontal");
        // Forward movement
        float ver = Input.GetAxis("Vertical");
        // Rotation along the horizon
        float azi = Input.GetAxis("Mouse X") * lookSensitivity;
        // Up and down rotation
        float ele = Input.GetAxis("Mouse Y") * lookSensitivity;

        // If player is not talking, they can move
        if (!talking)
        {
            // Ground check
            bool isGrounded = ctrl.isGrounded;

            // Increase fall speed if ungrounded
            if (isGrounded)
                fallSpeed = 0.0f;
            else
                fallSpeed += (acceleration * gravityMultiplier) * Time.deltaTime;

            // Look elevation is stored as a float due to the way Unity handles rotations (this is just easier)
            lookElevation = Mathf.Clamp(lookElevation -= ele, -89f, 89f);

            // Normalizes walk vector
            Vector3 walkVector = new Vector3(hor, 0, ver).normalized;

            // Makes sure walkVector behaves normally after being normalized
            walkVector = new Vector3(walkVector.x * Mathf.Abs(hor), 0, walkVector.z * Mathf.Abs(ver));

            // Checks if player is moving
            vel = ctrl.velocity.magnitude;

            // If player is grounded and moving, steps will occur
            if (isGrounded && vel > 1f)
            {
                if (!stepping)
                {
                    stepping = true;
                    StartCoroutine(PlayFootstep());
                }
            }

                // Moves character in all directions
                if (!GameController.holdPlayer)
                    ctrl.Move((transform.TransformVector(walkVector * walkSpeed) + Vector3.up * fallSpeed) * Time.deltaTime);

            // Rotates camera
            if (!GameController.holdPlayer)
            {
                cam.transform.eulerAngles = new Vector3(lookElevation, this.transform.eulerAngles.y, 0f);

                // Rotates player's body side-to-side
                transform.eulerAngles += Vector3.up * azi;
            }
        }
    }

    IEnumerator PlayFootstep()
    {
        yield return new WaitForSeconds(stepTime);

        if (vel > 1f)
        {
            aSource.pitch = Random.Range(0.625f, 0.675f);
            aSource.PlayOneShot(step);
        }
        stepping = false;
    }

    private void LateUpdate()
    {
        // Positions camera
        cam.transform.position = this.transform.position + Vector3.up * 0.5f;
    }
}
