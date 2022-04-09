using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    private Camera main;
    private Dictionary<int, interactableBase> interactableCollection;
    private Dictionary<int, bool> collisionTracking;
    private interactableBase currentlyHighlighted;
    private interactableBase pickedUpItem;
    public LayerMask interactableLayer;
    public Vector3 pickupDistance;

    public void Awake()
    {
        interactableCollection = new Dictionary<int, interactableBase>();
        collisionTracking = new Dictionary<int, bool>();

        main = Camera.main;
    }

    public void Update()
    {
        if (interactableCollection.Count != 0 && pickedUpItem == null)
        {
            interactableBase current = null;
            float dot = 0.0f;
            foreach (KeyValuePair<int, interactableBase> element in interactableCollection)
            {
                float _dot = Vector3.Dot(main.transform.forward, (element.Value.transform.position - main.transform.position).normalized);
                if (_dot > dot)
                {
                    if (_dot >= .995f)
                    {
                        current = element.Value;
                        dot = _dot;
                    }
                }
            }

            if (currentlyHighlighted != current)
            {
                if (currentlyHighlighted != null)
                    currentlyHighlighted.GetOnHover = false;
                currentlyHighlighted = current;
                if (currentlyHighlighted != null)
                    currentlyHighlighted.GetOnHover = true;
            }
        }
        else
        {
            if (currentlyHighlighted != null)
                currentlyHighlighted.GetOnHover = false;
            currentlyHighlighted = null;
        }
        if (Input.GetMouseButtonDown(0) && currentlyHighlighted != null || Input.GetMouseButtonDown(0) && pickedUpItem != null)
        {
            if (pickedUpItem == null)
            {
                pickedUpItem = currentlyHighlighted;
                currentlyHighlighted.GetInteractable.onEnter();
            }
            else
            {
                pickedUpItem.GetInteractable.onExit();
                pickedUpItem = null;
            }
        }

        if (Input.GetMouseButtonDown(1) && pickedUpItem != null)
        {
            pickedUpItem.GetInteractable.onAction();
        }
    }

    public void FixedUpdate()
    {
        Dictionary<int, bool> temporaryChecker = new Dictionary<int, bool>();
        foreach(KeyValuePair<int, bool> element in collisionTracking)
        {
            temporaryChecker.Add(element.Key, false);
        }

        Collider[] interactableCollectionInRange = Physics.OverlapBox(transform.position, pickupDistance, Quaternion.identity, interactableLayer);
        foreach (Collider element in interactableCollectionInRange)
        {
            if (interactableCollection.ContainsKey(element.gameObject.GetInstanceID()))
            {
                temporaryChecker[element.gameObject.GetInstanceID()] = true;
                continue;
            }
            else
            {
                temporaryChecker.Add(element.gameObject.GetInstanceID(), true);
                interactableCollection.Add(element.gameObject.GetInstanceID(), element.GetComponent<interactableBase>());
            }
        }

        List<int> tracker = new List<int>();
        foreach (KeyValuePair<int, bool> element in temporaryChecker)
        {
            if (element.Value == false)
            {
                tracker.Add(element.Key);
                interactableCollection.Remove(element.Key);
            }
        }

        collisionTracking = temporaryChecker;
        foreach(int element in tracker)
        {
            collisionTracking.Remove(element);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, pickupDistance);
    }
}
