using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaterDepth : MonoBehaviour
{

    [Header("Player Items")]
    GameObject playerObject;
    Transform playerTransform;
    [SerializeField]
    float player_y_value;
    [Header("Water Items")]
    GameObject waterObject;
    Transform waterObjectTransform;
    float waterObject_y_value;
    [Header("Results")]
    [SerializeField]
    double depthValue;
    public bool playerIsInWater;
    // Start is called before the first frame update
    void Start()
    {
        InitObjects();
    }

    void InitObjects()
    {
        playerObject = GameObject.Find("ThirdPersonController");
        playerTransform = playerObject.transform;

        waterObject = this.gameObject;
        waterObjectTransform = waterObject.transform;
        waterObject_y_value = waterObjectTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        CalcDepth();
    }

    void CalcDepth()
    {
        player_y_value = playerTransform.position.y;
        depthValue = System.Math.Round((waterObject_y_value - player_y_value), 2);
    }

    void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerIsInWater = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerIsInWater = false;
        }
    }
}
