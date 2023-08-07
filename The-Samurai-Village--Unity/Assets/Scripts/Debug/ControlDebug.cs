using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovementDebug();
        InteractionDebug();
        DataDebug();
    }

    void MovementDebug()
    {

    }

    //Interaction Bools
    [SerializeField]
    bool backspaceBool = false;
    void InteractionDebug()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            backspaceBool = true;

        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            backspaceBool = false;
        }
    }

    [Header("Time")]
    [SerializeField]
    float time;
    [SerializeField]
    float unscaledTime;
    [SerializeField]
    float deltaTime;
    [SerializeField]
    float unscaledDeltaTime;
    void DataDebug()
    {
        time = Time.time;
        unscaledTime = Time.unscaledTime;
        deltaTime = Time.deltaTime;
        unscaledDeltaTime = Time.unscaledDeltaTime;
    }

}


