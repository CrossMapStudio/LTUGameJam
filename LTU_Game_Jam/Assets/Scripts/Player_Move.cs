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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed;
        float ver = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed;
        float azi = Input.GetAxis("Mouse X") * lookSensitivity;
        float ele = Input.GetAxis("Mouse Y") * lookSensitivity;

        Vector3 move = new Vector3 (hor, 0f, ver);

        lookElevation = Mathf.Clamp(lookElevation -= ele, -89f, 89f);

        ctrl.Move(transform.forward * ver + transform.right * hor);

        cam.transform.rotation = Quaternion.Euler(lookElevation * lookSensitivity, 0, 0);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + (azi * lookSensitivity), 0);
    }
}
