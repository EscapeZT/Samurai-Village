using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour

// This script warps the player to locations for testing purposes as is not designed to be used in a final game. 

{
    // Start is called before the first frame update
    [SerializeField]
    Transform warpMarker1, warpMarker2, warpMarker3, warpMarker4, warpMarker5, warpMarker6;
    [SerializeField]
    Transform playerStartLocation;
    [SerializeField]
    GameObject player;

    void Start()
    {
         player = GameObject.Find("ThirdPersonController");
        //GetAndSetTransform(playerStartLocation,"PlayerStartMarker");  
        warpMarker1 = GameObject.Find("WarpMarker1").transform;
        warpMarker2 = GameObject.Find("WarpMarker2").transform;
        warpMarker3 = GameObject.Find("WarpMarker3").transform;
        warpMarker4 = GameObject.Find("WarpMarker4").transform;
        warpMarker5 = GameObject.Find("WarpMarker5").transform;
        warpMarker6 = GameObject.Find("WarpMarker6").transform;

        playerStartLocation = GameObject.Find("PlayerStartMarker").transform;

        player.transform.position = playerStartLocation.position;
        player.transform.rotation = playerStartLocation.rotation;     
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.transform.position = warpMarker1.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.transform.position = warpMarker2.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.transform.position = warpMarker3.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.transform.position = warpMarker4.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.transform.position = warpMarker5.position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            player.transform.position = warpMarker6.position;
        }
    }

}
