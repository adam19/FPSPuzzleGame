using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractionComponent : MonoBehaviour {

    public Text hudText;
    public float maxInteractionDistance = 1.5f;

    private Camera cameraComponent;

    // Use this for initialization
    void Start () {
        hudText.text = "";
        cameraComponent = GetComponentInChildren<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        hudText.text = "";

        RaycastHit HitInfo;
        if (Physics.Raycast(cameraComponent.transform.position, cameraComponent.transform.forward, out HitInfo, maxInteractionDistance))
        {
            InteractableInterface NearbyInterface = HitInfo.collider.GetComponentInParent<InteractableInterface>();
            
            if (NearbyInterface)
            {
                hudText.text = NearbyInterface.GetInteractionMessage(gameObject);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {                    
                    NearbyInterface.ActivateInterface(gameObject);
                }
            }
        }
    }

    public void DisplayInteractionMessage(InteractableInterface Interactable)
    {
        hudText.text = "";

        if (Interactable)
        {
            hudText.text = Interactable.uiText;
        }
    }
}
