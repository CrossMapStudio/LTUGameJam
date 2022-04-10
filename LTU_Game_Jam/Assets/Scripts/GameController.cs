using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public static Dictionary<string, Action> gameEventHandler;

    public Canvas currentCanvas;
    public static bool holdPlayer;

    public void Awake()
    {
        controller = this;
        if (gameEventHandler == null)
            gameEventHandler = new Dictionary<string, Action>();
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

    public static void addEvent(string ID, Action action)
    {
        gameEventHandler.Add(ID, action);
    }
}
