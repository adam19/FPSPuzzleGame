using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOverlay : MonoBehaviour {

    private float m_FadeOutTime = 1.0f;
    private float m_DelayToFade = 0.0f;
    private bool m_bIsFading = false;
    private Material m_CameraMaterial;

    // Use this for initialization
    void Start () {
        Renderer Rend = GetComponent<Renderer>();
        m_CameraMaterial = Rend.sharedMaterial;

        // Ensure the plane is fully transparent
        Color CurrentColor = m_CameraMaterial.color;
        CurrentColor.a = 0.0f;
        m_CameraMaterial.color = CurrentColor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_bIsFading)
        {
            // Preventing divide-by-zero
            if (m_FadeOutTime < 0.0001f)
            {
                m_FadeOutTime = 0.0001f;
            }

            Color CurrentColor = m_CameraMaterial.color;
            CurrentColor.a += (Time.deltaTime / m_FadeOutTime);

            if (CurrentColor.a >= 1.0f)
            {
                CurrentColor.a = 1.0f;
                m_bIsFading = false;
                StartCoroutine(_EndFadingOut());
            }

            m_CameraMaterial.color = CurrentColor;
        }
    }

    public void StartFadingOut(float FadeOutTime, float DelayFadeTime = 0.0f)
    {
        m_FadeOutTime = FadeOutTime;
        m_DelayToFade = DelayFadeTime;

        StartCoroutine(_StartFadingOut());
    }

    IEnumerator _StartFadingOut()
    {
        Debug.Log("StartFadingOut()");
        yield return new WaitForSeconds(m_DelayToFade);
        m_bIsFading = true;
    }

    IEnumerator _EndFadingOut()
    {
        Debug.Log("EndFadingOut()");
        yield return new WaitForSeconds(m_DelayToFade);
        Application.Quit();
    }
}
