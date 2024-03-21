using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    #region Objects & Variables
    [Header("FMOD Settings")]
    public FMODUnity.EventReference footstepEvent;
    FMOD.Studio.EventInstance footstepEventInstance;

    //PARAMETER IDS
    FMOD.Studio.PARAMETER_ID surfaceTypeID, movementTypeID;
    string surfaceTypeFMODParameter = "SurfaceType";
    string movementTypeFMODParameter = "MovementTyle";

    //GameObjects and Mechanics
    GameObject playerObject;
    Rigidbody playerRB;
    //Velocity
    Vector3 v;
    float playerVelocity;

    //Footstep Object
    GameObject footstepObject;
    Transform footstepObjectTransform;

    //Timer
    float timer = 0f;

    [Header("FMOD INFO")]
    public bool footstepActive;
    [Header("Gating")]
    public float resetThreshold;
    public float movementGate;

    //Surfaces
    [Header("Surfaces")]
    [SerializeField]
    string[] surfaceType = {"Grass", "Gravel", "Stone", "Water", "Deep Water", "Wood", "Rice Field", "Sand"};
    [SerializeField]
    string terrainTag;
    float surfaceTypeFloat = 0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        FMODSetup();
    }

    void FMODSetup()
    {
        playerObject = GameObject.Find("ThirdPersonController");
        playerRB = playerObject.GetComponent<Rigidbody>();

        footstepObject = GameObject.Find("FootstepAudioPlaybackObject");
        footstepObjectTransform = footstepObject.transform;

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstepEventInstance, footstepObjectTransform);
        FMOD.Studio.EventDescription footstepEventDescription = FMODUnity.RuntimeManager.GetEventDescription(footstepEvent);

        FMOD.Studio.PARAMETER_DESCRIPTION surfaceTypeParameterDescription;
        footstepEventDescription.getParameterDescriptionByName(surfaceTypeFMODParameter, out surfaceTypeParameterDescription);
        surfaceTypeID = surfaceTypeParameterDescription.id;

        FMOD.Studio.PARAMETER_DESCRIPTION movementTypeParameterDescription;
        footstepEventDescription.getParameterDescriptionByName(movementTypeFMODParameter, out movementTypeParameterDescription);
        movementTypeID = movementTypeParameterDescription.id;

    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerVelocity();
        Timer();
        RaycastSwitch();
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
            timer += (2 * Time.deltaTime);
            if (timer > resetThreshold)
            {
                timer = 0f;
                footstepActive = false;
            }
        }
    }

    void RaycastSwitch()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            terrainTag = hit.collider.gameObject.tag;
        }

        foreach (string i in surfaceType)
        {
            if (terrainTag == i)
            {
                float f = Array.IndexOf(surfaceType, i);
                surfaceTypeFloat = f;
            }
        }
    }

    void PlayFootstep()
    {
        if(!footstepActive)
        {
            if (playerVelocity > movementGate)
            {
                footstepEventInstance = FMODUnity.RuntimeManager.CreateInstance(footstepEvent);

                footstepEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(footstepObject));

                //footstepEventInstance.setParameterByID(movementTypeID, movementTypeFloat);
                footstepEventInstance.setParameterByID(surfaceTypeID, surfaceTypeFloat);

                footstepEventInstance.start();
                footstepEventInstance.release();

                footstepActive = true;
            }
        }
    }
}
