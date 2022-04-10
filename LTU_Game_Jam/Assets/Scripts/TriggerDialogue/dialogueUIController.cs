using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueUIController : MonoBehaviour
{
    public Text npcName, npcDialogue;
    private Dialogue dialogue_data;
    public Dialogue GetSetDialogueData { get { return dialogue_data; } set { dialogue_data = value; } }
    private int indexer;

    public delegate void dialogueEnd();
    public dialogueEnd end;

    private Animator anim;
    [SerializeField] private string animationToPlayOnIndex = "dialogueIndexChange";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void setUI()
    {
        npcName.text = dialogue_data.npcName;
        npcDialogue.text = dialogue_data.dialogues[indexer].dialogueContent;
    }

    public void nextIndexer()
    {
        indexer++;
        if (indexer >= dialogue_data.dialogues.Length)
        {
            end();
            Destroy(gameObject);
        }
        else
        {
            anim.Play(animationToPlayOnIndex, 0, 0f);
            setUI();
        }
    }
}
