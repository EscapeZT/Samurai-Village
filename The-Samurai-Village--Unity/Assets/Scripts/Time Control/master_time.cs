using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class master_time : MonoBehaviour
{
    [Header("Master Time")]
    [SerializeField]
    float master_timescale = 1f;
    [SerializeField]
    float delta_Time;

    [Header("Other Time")]
    public float object_timescale = 1f;
    public float menu_timescale = 1f;

    [Header("Pausing")]
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        IntialiseTime();
    }

    // Update is called once per frame
    void Update()
    {
        TimeDebug();
        PauseMenuTime(isPaused);
    }

    void IntialiseTime()
    {
        object_timescale = 1f;
        menu_timescale = 1f;
        master_timescale = 1f;

        master_timescale = Time.timeScale;
    }

    void TimeDebug()
    {
        delta_Time = Time.deltaTime;
    }

    void PauseMenuTime(bool Paused)
    {
        if (Paused)
        {
            object_timescale = 0f;
        }
        else if (!Paused)
        {
            object_timescale = 1f;
        }
    }

}
