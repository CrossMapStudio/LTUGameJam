using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "ScriptableObjects/Book")]
public class BookData : ScriptableObject
{
    public string title;
    public BookPage[] pages;
}

[System.Serializable]
public class BookPage
{
    [TextArea(3,5)]
    public string pageContent;
}
