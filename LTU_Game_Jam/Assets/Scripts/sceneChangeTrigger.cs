using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChangeTrigger : MonoBehaviour
{
    public string ID;
    public int sceneToChangeTo;
    bool disable;
    public CollectableData data;

    public void Awake()
    {
        if (GameController.checkActionTracker(ID))
        {
            disable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!disable)
        {
            if (other.tag == "Player")
            {
                GameController.addActionToTracker(ID);
                if (data != null)
                    inventoryPlayerController.addItemToInventory(data);
                SceneManager.LoadScene(sceneToChangeTo);
            }
        }
    }
}
