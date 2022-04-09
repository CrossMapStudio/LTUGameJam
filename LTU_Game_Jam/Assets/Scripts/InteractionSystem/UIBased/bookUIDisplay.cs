using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bookUIDisplay : MonoBehaviour
{
    public Text title, content, pageNumber;
    private BookData data;
    public BookData SetGetData { get { return data; } set { data = value; } }
    private int pageIndexer;

    public void setUI()
    {
        title.text = data.title;
        content.text = data.pages[pageIndexer].pageContent;
        pageNumber.text = (pageIndexer + 1).ToString() + "/" + data.pages.Length.ToString();
    }

    public void nextPage()
    {
        if (pageIndexer + 1 < data.pages.Length)
        {
            pageIndexer++;
            setUI();
        }
    }

    public void previousPage()
    {
        if (pageIndexer - 1 >= 0)
        {
            pageIndexer--;
            setUI();
        }
    }
}
