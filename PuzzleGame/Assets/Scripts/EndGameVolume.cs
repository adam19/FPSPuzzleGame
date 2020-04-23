using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EndGameVolume : MonoBehaviour
{
    public Text hudGameEndText;
    public float fadeOutTime = 5.0f;
    public float delayToFade = 2.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (hudGameEndText && hudGameEndText.gameObject)
        {
            hudGameEndText.gameObject.SetActive(true);
        }

        if (other && other.gameObject)
        {
            // Start fading out the player's camera
            ScreenOverlay overlay = other.gameObject.GetComponentInChildren<ScreenOverlay>();
            if (overlay)
            {
                overlay.StartFadingOut(fadeOutTime, delayToFade);
            }
        }
    }
}
