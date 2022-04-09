using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableBase : MonoBehaviour
{
    //For Testing Rn
    private MeshRenderer renderer;
    private interactable interactableObject;
    public interactable GetInteractable { get { return interactableObject; } }
    private bool onHover;
    public bool GetOnHover { get { return onHover; } set { onHover = value; } }
    //Scriptable Object Data for all instance types->
    [SerializeField] private enum interactableType
    {
        pickup,
        collectable,
        book,
    }
    [SerializeField] private interactableType type;
    public void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.material.color = Color.blue;
        switch (type)
        {
            case interactableType.pickup:
                break;
            case interactableType.collectable:
                break;
            case interactableType.book:
                interactableObject = new Book(this);
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        if (onHover)
        {
            renderer.material.color = Color.cyan;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    }
}

#region Abstract Structure
public abstract class interactable
{
    public interactableBase baseInteractable;
    public interactable(interactableBase _baseInteractable)
    {
        baseInteractable = _baseInteractable;
    }

    public virtual void onEnter()
    {
        Debug.Log("Base Class Call");
    }

    public virtual void onAction()
    {
        Debug.Log("Base Class Call");
    }

    public virtual void onExit()
    {
        Debug.Log("Base Class Call");
    }
}

#region Interactable Derived
public class Book : interactable
{
    public Book(interactableBase _baseInteractable) : base(_baseInteractable){}
    public override void onEnter()
    {
        Debug.Log("Book Class Call Enter");
    }

    public override void onAction()
    {
        Debug.Log("Book Class Call Action");
    }

    public override void onExit()
    {
        Debug.Log("Book Class Call Exit");
    }
}

public class Pickup : interactable
{
    public Pickup(interactableBase _baseInteractable) : base(_baseInteractable) {}
    public override void onEnter()
    {
        Debug.Log("Pickup Class Call Enter");
    }

    public override void onAction()
    {
        Debug.Log("Pickup Class Call Action");
    }

    public override void onExit()
    {
        Debug.Log("Pickup Class Call Exit");
    }
}

public class Collectable : interactable
{
    public Collectable(interactableBase _baseInteractable) : base(_baseInteractable) { }
    public override void onEnter()
    {
        Debug.Log("Collectable Class Call Enter");
    }

    public override void onAction()
    {
        Debug.Log("Collectable Class Call Action");
    }

    public override void onExit()
    {
        Debug.Log("Collectable Class Call Exit");
    }
}
#endregion
#endregion