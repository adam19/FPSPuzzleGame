using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInterface : MonoBehaviour {

    public struct Option
    {
        string Key;
        string Description;
    };
    
    public string uiText;
    public Option[] options;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other && other.gameObject)
        {
            InteractionComponent otherInteractor = other.gameObject.GetComponent<InteractionComponent>();
            if (otherInteractor)
            {
                //Debug.Log("OnTriggerEnter:");
                otherInteractor.DisplayInteractionMessage(this);
            }
        }
    }

    public virtual string GetInteractionMessage(GameObject interactor)
    {
        return uiText;
    }

    public virtual void ActivateInterface(GameObject interactor)
    {

    }
}
