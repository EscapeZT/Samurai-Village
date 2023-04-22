using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFO
Add this script to any water collider that has trigger enabled.
Ensure you set the WaterType correct.
Playback is changed in the Footstep Script and FMOD.
*/

public class WaterTrigger : MonoBehaviour
{

    GameObject player;
    FootstepsAudio footstepScript;
    public float waterTypeValue;

    void Start(){
        player = GameObject.Find("ThirdPersonController");
        footstepScript = player.GetComponent<FootstepsAudio>();
    }
    void OnTriggerStay()
    {
        footstepScript.playerIsInWater = true;
        footstepScript.waterType = waterTypeValue;
        
    }

    void OnTriggerExit()
    {
        footstepScript.playerIsInWater = false;
    }

    

}
