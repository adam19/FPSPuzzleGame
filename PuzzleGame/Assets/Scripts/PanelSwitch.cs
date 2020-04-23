using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : InteractableInterface
{
    public SwitchPanel parentPanel;
    public AudioClip pressedSound;
    public int switchIndex = -1;

    private AudioSource audioSouce;

    // Use this for initialization
    void Start () {
        audioSouce = GetComponent<AudioSource>();
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
        if (parentPanel)
        {
            parentPanel.SwitchActivated(this);
        }

        audioSouce.PlayOneShot(pressedSound);
    }
}
