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
    private Camera main;
    public Camera GetMain { get { return main; } }
    public bool GetOnHover { get { return onHover; } set { onHover = value; } }

    private bool isGrounded = true;
    private Rigidbody rb;
    public bool GetSetGrounded { get { return isGrounded; } set { isGrounded = value; } }
    [SerializeField] private enum interactableType
    {
        pickup,
        collectable,
        book,
    }
    [SerializeField] private interactableType type;

    [Header("Book")]
    public BookData Book_Data;
    public bookUIDisplay UIBookDataDisplay;


    public void Awake()
    {
        main = Camera.main;
        renderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
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

        interactableObject.onUpdate();
    }

    public void FixedUpdate()
    {
        interactableObject.onFixedUpdate();
    }

    public void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        rbUnfreeze();
    }

    public void rbFreeze()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void rbUnfreeze()
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}

#region Abstract Structure
public abstract class interactable
{
    public interactableBase baseInteractable;
    public Transform targetPosition;
    public interactable(interactableBase _baseInteractable)
    {
        baseInteractable = _baseInteractable;
    }

    public virtual void onEnter()
    {
        Debug.Log("Base Class Call");
        targetPosition = baseInteractable.GetMain.transform;
        baseInteractable.GetComponent<Collider>().enabled = false;
    }

    public virtual void onUpdate()
    {
        if (targetPosition != null)
            baseInteractable.transform.position = Vector3.Lerp(baseInteractable.transform.position, targetPosition.position + (baseInteractable.GetMain.transform.forward * .5f), Time.deltaTime * 10f);
    }

    //Trigger for Grounded -
    public virtual void onFixedUpdate()
    {
        if (!baseInteractable.GetSetGrounded)
        {
            baseInteractable.transform.position += Vector3.up * -2f * Time.deltaTime;
        }
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
    public BookData data;
    private bookUIDisplay bookUI = null;
    public Book(interactableBase _baseInteractable) : base(_baseInteractable){
        data = baseInteractable.Book_Data;
    }
    public override void onEnter()
    {
        Debug.Log("Book Class Call Enter");
        targetPosition = baseInteractable.GetMain.transform;
        baseInteractable.GetComponent<Collider>().enabled = false;
        baseInteractable.rbFreeze();
    }

    public override void onAction()
    {
        Debug.Log("Book Class Call Action");
        if (bookUI == null)
        {
            //Instantiate the Canvas Element ---
            bookUI = Object.Instantiate(baseInteractable.UIBookDataDisplay,GameController.controller.currentCanvas.transform.position, Quaternion.identity, GameController.controller.currentCanvas.transform);
            bookUI.SetGetData = data;
            bookUI.setUI();
        }
    }

    public override void onExit()
    {
        targetPosition = null;
        baseInteractable.GetComponent<Collider>().enabled = true;
        baseInteractable.GetSetGrounded = false;
        if (bookUI != null)
            Object.Destroy(bookUI.gameObject);
        //RigidBody Needed
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