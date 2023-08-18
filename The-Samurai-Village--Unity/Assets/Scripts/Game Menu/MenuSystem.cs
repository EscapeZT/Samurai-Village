using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
/*
    This script controls the menu system
*/
public class MenuSystem : MonoBehaviour
{
    [SerializeField]
    bool pauseState, allowPauseInOut, startReset;
    [SerializeField]
    float reset = 0.0f, resetMax = 5f;

    MenuCameras m_Cameras;
    MenuCameraTrigger m_CameraTrigger;

    // Start is called before the first frame update
    void Start()
    {
        allowPauseInOut = true;
        m_Cameras = GameObject.Find("OtherCameras").GetComponent<MenuCameras>();
        m_CameraTrigger = GameObject.Find("MenuCameraTrigger").GetComponent<MenuCameraTrigger>();
        pauseState = false;
        startReset = false;
        MainPauseMenuSetup();
    }

    // Update is called once per frame
    void Update()
    {
        PauseSystem();
        ResetPauseAllow();
        MainPauseMenuControl();
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
            //Time.timeScale = 0f;
        }
        else
        {
            m_Cameras.isInPauseMenu = false;
            //Time.timeScale = 1f;
        }

    }

    void ResetPauseAllow()
    {
        if (startReset)
        {
            reset = reset += 10f * Time.unscaledDeltaTime;
            allowPauseInOut = false;
        }

        if (reset > resetMax)
        {
            startReset = false;
            allowPauseInOut = true;
            reset = 0f;
        }

    }

    /* ----------------------------------------------------------------
    MAIN PAUSE MENU
    ---------------------------------------------------------------- */
    CanvasGroup pauseMenuCanvasGroup;
    public float exitMenuFadeSpeed;
    public float enterMenuFadeSpeed;

    void MainPauseMenuSetup(){
        GameObject menutempobject = GameObject.Find("Pause Menu Canvas");
        pauseMenuCanvasGroup = menutempobject.GetComponent<CanvasGroup>();
    }

    void MainPauseMenuControl(){
        float menuAlphaFloat = pauseMenuCanvasGroup.alpha;
        
        if(m_Cameras.isInPauseMenu && m_CameraTrigger.inTriggerZone)
        {
            if(menuAlphaFloat <=1f)
            {
                menuAlphaFloat += enterMenuFadeSpeed*Time.deltaTime;
            }
        } else if(!m_Cameras.isInPauseMenu && !m_CameraTrigger.inTriggerZone)
        {
            if(menuAlphaFloat > 0f)
            {
                menuAlphaFloat -= exitMenuFadeSpeed*Time.deltaTime;
            }
        }

        

        pauseMenuCanvasGroup.alpha = menuAlphaFloat;
    }
}
