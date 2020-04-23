using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : InteractableInterface {

    public string m_ItemName = "";
    public string m_ItemType = "";

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override string GetInteractionMessage(GameObject Interactor)
    {
        return uiText;
    }

    public override void ActivateInterface(GameObject Interactor)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Add item to the other interactors inventory
        if (other)
        {
            InventoryManager InvManager = other.GetComponent<InventoryManager>();
            if (InvManager)
            {
                InvManager.AddItemToInventory(this);
                gameObject.SetActive(false);
            }
        }
    }
}
