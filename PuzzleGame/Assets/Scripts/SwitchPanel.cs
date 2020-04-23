using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanel : MonoBehaviour {

    public LightCodePanel lightCodePanel;
    public int numLights = 4;
    public Door doorToUnlock;
    public GameObject keyToDrop;
    public AudioClip successSound;
    public AudioClip errorSound;

    private List<int> activationOrder;
    private int activationIndex = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        activationOrder = new List<int>(numLights);
        
        for (int i = 0; i < numLights; i++)
        {
            activationOrder.Add((int)Math.Floor(UnityEngine.Random.value * 2.9f));

            Color lightColor = Color.white;
            switch (activationOrder[i])
            {
                case 0: lightColor = Color.red; break;
                case 1: lightColor = Color.green; break;
                case 2: lightColor = Color.blue; break;
            }
            lightCodePanel.AddLight(lightColor);
        }
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchActivated(PanelSwitch activatedSwitch)
    {
        if (activatedSwitch)
        {
            if (activationOrder[activationIndex] == activatedSwitch.switchIndex)
            {
                ++activationIndex;

                //Debug.Log("Valid entry");
            }
            else
            {
                // Invalid entry, reset activation order
                activationIndex = 0;
                audioSource.PlayOneShot(errorSound);

                //Debug.Log("Invalid sequence. Resetting");
            }

            if (activationIndex == numLights)
            {
                // Successful sequence entered
                audioSource.PlayOneShot(successSound);

                if (keyToDrop)
                {
                    keyToDrop.SetActive(true);
                }
            }
        }
    }
}
