using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MenuCameras : MonoBehaviour
{
    public bool isInPauseMenu = false;
    public bool exitPauseMenu;
    public bool menuCameraReset = false;
    Transform menuCamera1Transform;
    GameObject menuCamera1;
    GameObject mainCamera;
    GameObject inGameUIObject;

    public Transform camera1TransformStart;
    public Transform camera1TransformPosition1;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraFade = false;
        SetupCameras();
        blackOutImage = GameObject.Find("BlackOut").GetComponent<Image>();
        var tempColor = blackOutImage.color;
        tempColor.a = 0f;
        blackOutImage.color = tempColor;

        inGameUIObject = GameObject.Find("In Game UI");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ResetCamera1();
        TurnOffMainCamera();
        MenuLoadCameraMove();
    }

    void SetupCameras()
    {
        exitPauseMenu = false;
        menuCamera1 = GameObject.Find("MenuCamera1");
        menuCamera1.transform.position = camera1TransformStart.position;
        menuCamera1.SetActive(false);
        //GetMainCamera
        mainCamera = GameObject.Find("Camera");
    }

    float timeResetModifier = 1f;
    float timeResetMax = 10f;
    void ResetCamera1()
    {

        /*
        if (!isInPauseMenu)
        {
            menuCamera1.transform.position = camera1TransformStart.position;
        }
        */
    }
    float movementSpeed = 1f;
    void MenuLoadCameraMove()
    {
        if (isInPauseMenu)
        {
            menuCamera1.transform.position = Vector3.Lerp(menuCamera1.transform.position, camera1TransformPosition1.position, Time.unscaledDeltaTime);
        }
    }

    [SerializeField]
    bool mainCameraFade;
    [SerializeField]
    float tempColorFloat;
    Image blackOutImage;
    void TurnOffMainCamera()
    {
        var tempColor = blackOutImage.color;
        tempColorFloat = tempColor.a;

        //check if in pause mode (controlled in MenuSystem.cs)
        if (isInPauseMenu)
        {
            //check if camera fade is complete to full black out.
            if (!mainCameraFade)
            {
                tempColor.a = Mathf.Lerp(tempColor.a, 1.1f, 4f * Time.unscaledDeltaTime);

                if (tempColor.a >= 1f)
                {
                    inGameUIObject.SetActive(false);
                    mainCameraFade = true;
                }
            }

            //if mainCameraFade complete, swap the cameras while screen is black.
            if (mainCameraFade)
            {
                mainCamera.SetActive(false);
                menuCamera1.SetActive(true);

                tempColor.a = Mathf.Lerp(tempColor.a, -0.1f, 4f * Time.unscaledDeltaTime);
            }

        }
        else if (!isInPauseMenu)
        {
            if (mainCameraFade)
            {
                menuCamera1.transform.position = Vector3.Lerp(menuCamera1.transform.position, camera1TransformStart.position, Time.unscaledDeltaTime);

                tempColor.a = Mathf.Lerp(tempColor.a, 1.1f, 4f * Time.unscaledDeltaTime);

                if (tempColor.a >= 1f)
                {
                    mainCameraFade = false;
                    inGameUIObject.SetActive(true);
                }
            }

            if (!mainCameraFade)
            {
                menuCamera1.SetActive(false);
                mainCamera.SetActive(true);

                tempColor.a = Mathf.Lerp(tempColor.a, -0.1f, 4f * Time.unscaledDeltaTime);
            }
        }

        blackOutImage.color = tempColor;
    }

    /*
    Camera Locations

    Camera 1 Start Location
    Vector3(38.6500015,58.5600014,-89.6999969)
    

    Camera 1 End Location
    Vector3(38.6500015,50.9399986,-89.6999969)

    
    */

}
