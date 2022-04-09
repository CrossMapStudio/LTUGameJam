using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public Canvas currentCanvas;
    public void Awake()
    {
        controller = this;
    }
}
