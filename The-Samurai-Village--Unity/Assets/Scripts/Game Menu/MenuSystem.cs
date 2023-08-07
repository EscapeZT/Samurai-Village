using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script controls the menu system
*/
public class MenuSystem : MonoBehaviour
{
    [SerializeField]
    bool pauseState, allowPauseInOut, startReset;
    [SerializeField]
    float reset = 0.0f, resetMax = 10f;

    MenuCameras m_Cameras;

    // Start is called before the first frame update
    void Start()
    {
        allowPauseInOut = true;
        m_Cameras = GameObject.Find("OtherCameras").GetComponent<MenuCameras>();
        pauseState = false;
        startReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        PauseSystem();
        ResetPauseAllow();
    }

    void PauseSystem()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && allowPauseInOut)
        {
            if(!pauseState)
            {
                pauseState = true;
            } else if (pauseState)
            {
                pauseState = false;
            }
          
            startReset = true;
        }


        if (pauseState)
        {
            m_Cameras.isInPauseMenu = true;
            Time.timeScale = 0.25f;
        }
        else
        {
            m_Cameras.isInPauseMenu = false;
            Time.timeScale = 1f;
        }

    }

    void ResetPauseAllow()
    {
        if (startReset)
        {
            reset = reset += 5f * Time.unscaledDeltaTime;
            allowPauseInOut = false;
        }

        if (reset > resetMax)
        {
            startReset = false;
            allowPauseInOut = true;
            reset = 0f;
        }

    }
}
