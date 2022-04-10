using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "ScriptableObjects/Collectable")]
public class CollectableData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite UIImage;
}
