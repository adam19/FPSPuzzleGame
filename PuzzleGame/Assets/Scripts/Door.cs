using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableInterface
{    
    public enum EDoorState
    {
        Closed,
        Opening,
        Opened,
        Closing
    };
    private EDoorState doorState = EDoorState.Closed;

    public float openedAngleInDegrees = 90.0f;
    public float timeToOpenInSeconds = 1.0f;
    public Vector3 pivotPosition;
    public string keyIdNeededToUnlock;
    public AudioClip unlockSound;
    public AudioClip openingSound;
    public string missingKeyMessage = "You do not have the correct key";
    public string unlockDoorMessage = "Unlock door?";
    public string closedDoorMessage = "Open?";
    public string openedDoorMessage = "Close?";

    private Quaternion closedQuat;
    private Quaternion openedQuat;
    private AudioSource audioSource;
    private float movementTime = 0.0f;
    private bool isUnlocked = false;

    // Use this for initialization
    void Start () {
        doorState = EDoorState.Closed;
        
        closedQuat = Quaternion.LookRotation(transform.forward, transform.up);
        openedQuat = Quaternion.Euler(0, openedAngleInDegrees, 0);

        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (doorState == EDoorState.Opening || doorState == EDoorState.Closing)
        {
            float t = movementTime / timeToOpenInSeconds;
            movementTime += Time.deltaTime;
            
            if (doorState == EDoorState.Opening)
            {
                if (t > 1.0f)
                {
                    doorState = EDoorState.Opened;
                }
            }
            else if (doorState == EDoorState.Closing)
            {
                t = 1.0f - t;

                if (t < 0.0f)
                {
                    doorState = EDoorState.Closed;
                }
            }
            
            transform.rotation = Quaternion.Slerp(closedQuat, openedQuat, t);
        }
	}

    public void ToggleDoor()
    {
        if (!isUnlocked)
        {
            return;
        }

        switch (doorState)
        {
            case EDoorState.Closed:
                doorState = EDoorState.Opening;
                movementTime = 0.0f;
                break;
            case EDoorState.Opened:
                doorState = EDoorState.Closing;
                movementTime = 0.0f;
                break;
        }

        audioSource.PlayOneShot(openingSound);

        //Debug.Log("Changed door state to " + doorState);
    }

    public override string GetInteractionMessage(GameObject interactor)
    {
        string message = "";
        
        if (interactor)
        {
            InventoryManager invManager = interactor.GetComponent<InventoryManager>();
            if (invManager && !invManager.HasItem(keyIdNeededToUnlock))
            {
                return missingKeyMessage;
            }

            if (!isUnlocked)
            {
                return unlockDoorMessage;
            }

            switch (doorState)
            {
                case EDoorState.Closed:
                    message = closedDoorMessage;
                    break;
                case EDoorState.Opened:
                    message = openedDoorMessage;
                    break;
            }
        }

        return message;
    }

    public override void ActivateInterface(GameObject interactor)
    {
        if (interactor)
        {
            InventoryManager invManager = interactor.GetComponent<InventoryManager>();

            if (invManager)
            {
                //Debug.Log("Valid InvManager - Needed Key=" + keyIdNeededToUnlock);
                if (invManager.HasItem(keyIdNeededToUnlock))
                {
                    if (!isUnlocked)
                    {
                        if (unlockSound)
                        {
                            audioSource.PlayOneShot(unlockSound);
                        }

                        isUnlocked = true;
                    }
                    else
                    {
                        ToggleDoor();
                    }
                }
            }
        }
    }
}
