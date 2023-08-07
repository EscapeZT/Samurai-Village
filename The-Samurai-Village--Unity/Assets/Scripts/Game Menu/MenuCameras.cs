using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameras : MonoBehaviour
{
    public bool isInPauseMenu = false; 
    Transform camera1Transform;
    GameObject menuCamera1;
    GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        SetupCameras();
    }

    // Update is called once per frame
    void Update()
    {
        ResetCamera1();
        TurnOffMainCamera();
        MenuLoadCameraMove();
    }

    void SetupCameras()
    {
        menuCamera1 = GameObject.Find("MenuCamera1");
        camera1Transform = menuCamera1.transform;
        camera1Transform.localPosition = camera1StartLocation;
        menuCamera1.SetActive(false);
        //GetMainCamera
        mainCamera = GameObject.Find("Camera");
    }

    float timeResetModifier = 1f;
    float timeResetMax = 10f;
    void ResetCamera1()
    {
        float t = 0f;
        if(!isInPauseMenu)
        {
            t = t+Time.deltaTime+timeResetModifier;
            if(t >= timeResetMax)
            {
                camera1Transform.localPosition = camera1StartLocation;
            }
        }
    }
    Vector3 camera1StartLocation = new Vector3(38.6500015f,58.5600014f,-89.6999969f);
    Vector3 camera1EndLocation = new Vector3(38.6500015f,50.9f,-89.6999969f);
    float movementSpeed = 1f; 

    void MenuLoadCameraMove()
    {
        if(isInPauseMenu)
        {
            camera1Transform.localPosition = new Vector3(camera1Transform.position.x,Mathf.Lerp(camera1StartLocation.y,camera1EndLocation.y,movementSpeed),camera1Transform.position.z);
        }
    }

    void TurnOffMainCamera()
    {
        if(isInPauseMenu)
        {
            mainCamera.SetActive(false);
            menuCamera1.SetActive(true);
        } else if (!isInPauseMenu)
        {
            menuCamera1.SetActive(false);
            mainCamera.SetActive(true);
        }
    }

    /*
    Camera Locations

    Camera 1 Start Location
    Vector3(38.6500015,58.5600014,-89.6999969)
    

    Camera 1 End Location
    Vector3(38.6500015,50.9399986,-89.6999969)

    
    */

}
