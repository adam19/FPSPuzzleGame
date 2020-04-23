using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : InteractableInterface
{
    public bool isExplored = false;
    public string notExploredMessage = "Examine contents?";
    public string exploredMessage = "You have already explored this box";
    public InventoryItem item;
    public AudioClip exploringSound;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public override string GetInteractionMessage(GameObject interactor)
    {
        if (isExplored)
        {
            return exploredMessage;
        }

        return notExploredMessage;
    }

    public override void ActivateInterface(GameObject interactor)
    {
        base.ActivateInterface(interactor);

        // Add item to the other interactors inventory
        if (interactor)
        {
            InventoryManager invManager = interactor.GetComponent<InventoryManager>();
            if (invManager)
            {
                if (item)
                {
                    invManager.AddItemToInventory(item);
                    item.gameObject.SetActive(false);
                }

                audioSource.PlayOneShot(exploringSound);
                isExplored = true;
            }
        }
    }
}
