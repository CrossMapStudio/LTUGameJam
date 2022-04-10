using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string npcName;
    public dialogue[] dialogues;
}

[System.Serializable]
public class dialogue
{
    public string dialogueContent;
}
