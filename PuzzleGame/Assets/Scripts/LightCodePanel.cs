using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCodePanel : MonoBehaviour {

    public GameObject lightPrefab;
    public Vector3 offset = new Vector3(0.0f, -1.0f, 0.0f);
    
    private Vector3 nextPosition;
    private List<Color> colorOrder;


	// Use this for initialization
    void Start () {
        nextPosition = transform.position;
        SpawnLights();
    }
    
	// Update is called once per frame
    void Update () {
        
    }
    
    public void SpawnLights()
    {
        for (int i = 0; i < colorOrder.Count; i++)
        {
            GameObject newDoorLight = Instantiate(lightPrefab, nextPosition, transform.rotation, transform);

            if (newDoorLight)
            {
                Light doorLight = newDoorLight.GetComponentInChildren<Light>();
                if (doorLight)
                {
                    doorLight.color = colorOrder[i];
                }
            }

            nextPosition += offset;
        }
    }
    
    public void AddLight(Color lightColor)
    {
        if (colorOrder == null)
        {
            colorOrder = new List<Color>();
        }
        
        colorOrder.Add(lightColor);
    }
}
