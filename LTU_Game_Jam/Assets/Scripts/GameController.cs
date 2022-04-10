using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public static Dictionary<string, Action> gameEventHandler;

    public static HashSet<string> actionTracker;

    public Canvas currentCanvas;
    public static bool holdPlayer;

    public void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        controller = this;
        if (gameEventHandler == null)
            gameEventHandler = new Dictionary<string, Action>();

        if (actionTracker == null)
            actionTracker = new HashSet<string>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1);
        }
    }

    public static void callEvent(string ID)
    {
        if (gameEventHandler.ContainsKey(ID))
        {
            //Execute the Function and Remove
            gameEventHandler[ID].Invoke();
            gameEventHandler.Remove(ID);
        }
    }

    public static void callEventOnCheckInventory(string invID, string eventID)
    {
        if (inventoryPlayerController.checkInventory(invID))
        {
            callEvent(eventID);
        }
    }

    public static void addEvent(string ID, Action action)
    {
        if (gameEventHandler.ContainsKey(ID))
        {
            return;
        }
        gameEventHandler.Add(ID, action);
    }

    public static void addEventAfterInvCheck(string invID, string ID, Action action)
    {
        if (inventoryPlayerController.checkInventory(invID))
        {
            addEvent(ID, action);
        }
    }

    public static void addActionToTracker(string ID)
    {
        actionTracker.Add(ID);
    }

    public static bool checkActionTracker(string ID)
    {
        if (actionTracker == null)
            actionTracker = new HashSet<string>();
        if (actionTracker.Contains(ID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
