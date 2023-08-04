using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script controls the menu system
*/
public class MenuSystem : MonoBehaviour
{
    bool pauseState;

    // Start is called before the first frame update
    void Start()
    {
        pauseState = false;
    }

    // Update is called once per frame
    void Update()
    {
        PauseSystem();
    }

    void PauseSystem(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(pauseState){
                pauseState = false;
            } else if(!pauseState){
                pauseState = true;
            }
        }

        if(pauseState){
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }
}
