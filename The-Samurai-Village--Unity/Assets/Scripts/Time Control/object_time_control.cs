using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_time_control : MonoBehaviour
{

    master_time m_master_time;
    // Start is called before the first frame update
    void Start()
    {
        m_master_time = GameObject.Find("Time Control").GetComponent<master_time>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PauseObject(bool isPaused)
    {
        if(isPaused)
        {
         
        }
    }
}
