using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Move : MonoBehaviour
{
    public Transform eye;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Rotates the eye toward the player from any angle (borrowed from NPCLook)
        var targetRotation = Quaternion.LookRotation((player.position + Vector3.up * 0.25f) - eye.position);
        // Smoothly rotate towards the target point.
        eye.rotation = Quaternion.Slerp(eye.rotation, targetRotation * Quaternion.Euler(Vector3.up * -90), Time.deltaTime * 3f);
    }
}
