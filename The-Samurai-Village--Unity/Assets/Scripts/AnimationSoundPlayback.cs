using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundPlayback : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter animationEvent;

    void PlaySound()
    {
        animationEvent.SendMessage("Play");
    }

}
