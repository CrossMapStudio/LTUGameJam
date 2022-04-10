using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deskTrigger : MonoBehaviour
{
    public Vector3 offset, halfExtents;
    public LayerMask triggerMask;

    int index = 0;
    public string[] tags = { "Apple", "RightAnswers" };
    private List<System.Action> actions;

    private GameObject stored;
    public Animator eyeballAnim;

    public CollectableData shard;

    public void Awake()
    {
        actions = new List<System.Action>();
        actions.Add(new System.Action(showEyeBall));
        actions.Add(new System.Action(openDoor));
    }

    public void FixedUpdate()
    {
        var coll = Physics.OverlapBox(transform.position + offset, halfExtents, transform.rotation, triggerMask);

        if (coll.Length >= 0)
        {
            foreach (Collider element in coll)
            {
                if (element.tag == tags[index])
                {
                    if (index < actions.Count)
                    {
                        stored = element.gameObject;
                        actions[index]();
                        index++;
                    }
                }
            }
        }
    }

    public void showEyeBall()
    {
        Destroy(stored);
        eyeballAnim.Play("eyeShow");
        //Play Sound do Animation ->

    }

    IEnumerator lookAtEyeBall()
    {
        yield return new WaitForSecondsRealtime(3f);
    }

    public void openDoor()
    {
        Destroy(stored);
        //Open Door Trigger ->
        GameController.callEvent("AnxietyDoor");
        inventoryPlayerController.addItemToInventory(shard);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, halfExtents);
    }
}
