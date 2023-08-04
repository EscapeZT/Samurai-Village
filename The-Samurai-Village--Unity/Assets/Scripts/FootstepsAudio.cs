using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* ASSESSMENT HELP !!!
This script has been created for you to power the footstep playback system.
Please look for the area called "Add playback code here".
This is where you will need to add your code to make the footstep system work.
If you need help, please watch the help video on Footsteps.
*/
public class FootstepsAudio : MonoBehaviour
{
    [Header("FMOD")]
    public FMODUnity.EventReference footstepEvent;
    FMOD.Studio.EventInstance footstepEventInstance;
    FMOD.Studio.PARAMETER_ID footstepTypeID, surfaceTypeID;
    string surfaceTypeParameter = "SurfaceType";
    string footstepTypeParameter = "FootstepType";
    public GameObject footstepSoundEmitterObject;
    public Transform footstepPlaybackPosition;

    // --- PlayerObjects & Rigidbody --- //
    GameObject playerObject;
    Rigidbody playerRB;
    Vector3 v; //used to collect player velocity;

    //Timer
    float t = 0f;

    [Header("Player Velocity")]
    [SerializeField]
    float playerVelocity;
    public float velocityGate = 0.15f;

    [Header("Movement Gate")]
    public float resetModifier = 0f;
    public float resetThreshold = 0.3f;
    public bool footstepActive;

    [Header("Footstep Type Switching")]
    [SerializeField]
    float footstepTypeFloat;

    void Start()
    {
        //InitalizeSystem
        GetObjects();
        InitializeFMOD();
    }

    void Update()
    {
        FootstepTypeSwitcher();
        //PlayFootstep();
        Timer();
        GetPlayerVelocity();
        SurfaceDetection();
    }

    void GetObjects()
    {
        playerObject = GameObject.Find("ThirdPersonController");
        playerRB = playerObject.GetComponent<Rigidbody>();

        footstepSoundEmitterObject = GameObject.Find("FootstepSoundEmitter");
        footstepPlaybackPosition = footstepSoundEmitterObject.transform;
    }

    void InitializeFMOD()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstepEventInstance, footstepPlaybackPosition);
        FMOD.Studio.EventDescription footstepEventDescription = FMODUnity.RuntimeManager.GetEventDescription(footstepEvent);
        FMOD.Studio.PARAMETER_DESCRIPTION footstepTypeParameterDescription;
        footstepEventDescription.getParameterDescriptionByName("FootstepType", out footstepTypeParameterDescription);
        footstepTypeID = footstepTypeParameterDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION surfaceTypeParameterDescription;
        footstepEventDescription.getParameterDescriptionByName("SurfaceType", out surfaceTypeParameterDescription);
        surfaceTypeID = surfaceTypeParameterDescription.id;
    }

    void GetPlayerVelocity()
    {
        v.x = Mathf.Abs(playerRB.velocity.x);
        v.z = Mathf.Abs(playerRB.velocity.z);

        playerVelocity = (v.x + v.z);
    }

    void Timer()
    {
        if (footstepActive)
        {
            t += (2 * Time.deltaTime) + resetModifier;
            if (t > resetThreshold)
            {
                t = 0f;
                footstepActive = false;
            }
        }
    }

    void PlayFootstep()
    {
        if (!footstepActive)
        {
            if (playerVelocity > velocityGate)
            {
                footstepEventInstance = FMODUnity.RuntimeManager.CreateInstance(footstepEvent);

                footstepEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(footstepSoundEmitterObject));

                footstepEventInstance.setParameterByID(footstepTypeID, footstepTypeFloat);

                footstepEventInstance.setParameterByID(surfaceTypeID, surfaceTypeIndex);

                // ADD YOUR CODE HERE

                footstepEventInstance.start();
                footstepEventInstance.release();

                footstepActive = true;

            }
        }
    }

    void FootstepTypeSwitcher()
    {
        if (playerVelocity > 3.5f)
        {
            footstepTypeFloat = 0f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            footstepTypeFloat = 1f;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            footstepTypeFloat = 2f;
        }
    }

    #region SurfaceAnalysis

    [Header("RayCast")]
    public string terrainTag;

    [SerializeField]
    string[] surfaceType = { "Grass", "Gravel", "Stone", "Wood", "Sand", "Water", "ShallowWater" };
    public float surfaceTypeIndex;

    [Header("Liquid Collision")]
    public bool playerIsInWater = false;
    public float waterType = 0f;

    void SurfaceDetection()
    {
        if (playerIsInWater)
        {
            //Index Values 5f = Water, 6f = Shallow Water

            if (waterType == 0f)
            {
                surfaceTypeIndex = 5f;
            }
            else if (waterType == 1f)
            {
                surfaceTypeIndex = 6f;
            }
        }
        else if(!playerIsInWater)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                terrainTag = hit.collider.gameObject.tag;
            }

            foreach (string i in surfaceType)
            {
                if (terrainTag == i)
                {
                    surfaceTypeIndex = Array.IndexOf(surfaceType, i);
                }
            }
        }
    }
    #endregion
}
