using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bedRoomTrigger : MonoBehaviour
{
    public Vector3 offset, halfExtents;
    public LayerMask triggerMask;

    int index = 0;
    public string[] tags = { "Book" };
    private int target = 3;
    private List<System.Action> actions;

    private GameObject stored;
    public CollectableData shard;

    public void Awake()
    {
        actions = new List<System.Action>();
        actions.Add(new System.Action(openDoor));
    }

    public void FixedUpdate()
    {
        var coll = Physics.OverlapBox(transform.position + offset, halfExtents, transform.rotation, triggerMask);

        if (coll.Length >= 0)
        {
            foreach (Collider element in coll)
            {
                if (element.tag == tags[0])
                {
                    index++;
                    Destroy(element.gameObject);
                }
            }

            if (index == 3)
            {
                openDoor();
                index = 0;
            }
        }
    }

    public void openDoor()
    {
        Destroy(stored);
        //Open Door Trigger ->
        GameController.callEvent("DoorBo");
        inventoryPlayerController.addItemToInventory(shard);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, halfExtents);
    }
}
