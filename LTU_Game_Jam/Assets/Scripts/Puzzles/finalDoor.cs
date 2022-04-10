using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalDoor : MonoBehaviour
{
    public Vector3 offset, halfExtents;
    public LayerMask triggerMask;

    private int index = 0;
    private int indexTarget = 3;

    public GameObject finalUI;
    public bool finalUIActive;

    public void Awake()
    {

    }

    public void FixedUpdate()
    {
        var coll = Physics.OverlapBox(transform.position + offset, halfExtents, transform.rotation, triggerMask);
        if (coll.Length >= 2)
        {
            //Loop Inventory and Check for 3 Shards ->
            foreach (KeyValuePair<string, CollectableData> element in inventoryPlayerController.invGet)
            {
                if (element.Value.itemID == "Shard1" || element.Value.itemID == "Shard2" || element.Value.itemID == "Shard3")
                {
                    index++;
                }
            }

            if (index == indexTarget && !finalUIActive)
            {
                //EndGame ->
                GameController.holdPlayer = true;
                Cursor.lockState = CursorLockMode.Confined;
                //Spawn the Option on the UI ->
                finalUIActive = true;
                finalUI.SetActive(true);
            }
            else
            {
                index = 0;
            }
        }
    }

    public void openDoor()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, halfExtents);
    }
}
