using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraTrigger : MonoBehaviour
{
    public bool inTriggerZone;
    // Start is called before the first frame update
    void Start()
    {
        inTriggerZone = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider camera)
    {
        if (camera.gameObject.tag == "MenuCamera")
        {
            inTriggerZone = true;
        }
    }

    void OnTriggerExit(Collider camera)
    {
        if (camera.gameObject.tag == "MenuCamera")
        {
            inTriggerZone = false;
        }
    }
}
