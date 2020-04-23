using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public AudioClip pickupSound;

    private List<InventoryItem> items;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        items = new List<InventoryItem>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool AddItemToInventory(InventoryItem newItem)
    {
        if (newItem)
        {
            //Debug.Log("Added " + NewItem.itemName + " to inventory!");

            items.Add(newItem);
            audioSource.PlayOneShot(pickupSound);
        }

        return true;
    }

    public bool HasItem(string itemName)
    {
        foreach (InventoryItem item in items)
        {
            if (item.m_ItemName == itemName)
            {
                return true;
            }
        }

        return false;
    }
}
