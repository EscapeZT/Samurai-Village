using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gong : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter gongEmitter;

    void OnTriggerStay(Collider player){
        if(player.gameObject.tag == "Player"){
            if(Input.GetKeyDown(KeyCode.E)){
                gongEmitter.SendMessage("Play");
            }
        }
    }
    
}
