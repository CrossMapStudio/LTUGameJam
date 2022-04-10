using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLook : MonoBehaviour
{
    private Transform player;
    public Transform headBone;
    public void Awake()
    {
        //Handle this is the game controller later
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        float param = Vector3.Dot(transform.forward, (player.position - transform.position).normalized);
        if (param >= .5f)
        {
            var targetRotation = Quaternion.LookRotation(player.position - headBone.position);
            // Smoothly rotate towards the target point.
            headBone.rotation = Quaternion.Slerp(headBone.rotation, targetRotation, Time.deltaTime * 3f);
        }
        else
        {
            headBone.rotation = Quaternion.Slerp(headBone.rotation, Quaternion.LookRotation(transform.forward), Time.deltaTime);
        }
    }
}
