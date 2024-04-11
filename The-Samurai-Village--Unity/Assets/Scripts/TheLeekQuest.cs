using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLeekQuest : MonoBehaviour
{
    [Header("Triggers")]
    public GameObject leekTrigger;
    public GameObject marketSellerTrigger;

    [Header("UI Elements")]
    public GameObject pressToTalkUI;
    public GameObject pressToPickUpUI;
    public GameObject pressToGiveUI;
    public GameObject leekUIImage;

    [Header("Items")]
    public GameObject leekToCollect;
    public GameObject leekToGive;
    public GameObject MarketSeller;
    public bool questStarted;
    bool doesPlayerHaveLeek;

    //FMOD Playback Locations
    Transform MarketSellerTransform;
    Transform pickupTransform;
    Transform putdownTransform;

    [Header("FMOD Events")]
    public FMODUnity.EventReference questStartEvent;
    public FMODUnity.EventReference questEndEvent;
    public FMODUnity.EventReference pickupEvent;
    public FMODUnity.EventReference putdownEvent;
    FMOD.Studio.EventInstance questStartEventInstance;
    FMOD.Studio.EventInstance questEndEventInstance;
    FMOD.Studio.EventInstance pickupEventInstance;
    FMOD.Studio.EventInstance putdownEventInstance;
    /*GAME AUDIO TIP
    Your FMOD & player objects should be defined here
    */

    void Start()
    {
        questStarted = false;
        pressToGiveUI.SetActive(false);
        pressToPickUpUI.SetActive(false);
        pressToTalkUI.SetActive(false);
        leekToGive.SetActive(false);
        leekUIImage.SetActive(false);

        FMODInitialiseEvents();
    }

    void Update()
    {
        LeekPickup();
        MarketSellerStart();
        MarketSellerEnd();
    }


    /* GAME AUDIO TIP
    MarketSellerStart starts The Leek Quest. 
    */

    void MarketSellerStart()
    {
        if (marketSellerTrigger.GetComponent<MarketSellerTrigger>().playerIsInMarketSellerTrigger == true && questStarted == false)
        {
            pressToTalkUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                LeekQuestAudio("Quest Start");

                questStarted = true;
                pressToTalkUI.SetActive(false);
            }
        }
    }

    /* GAME AUDIO TIP
    Leek pickup (below) checks if the leek can be collected (if the quest has started) 
    and if the player is in the leek collection trigger box by referencing the 
    bool found in LeekPickup.cs
    */

    void LeekPickup()
    {
        if (questStarted == true && leekTrigger.GetComponent<LeekPickup>().playerIsInLeekTrigger == true)
        {
            if (doesPlayerHaveLeek == false)
            {
                pressToPickUpUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    LeekQuestAudio("Pickup");

                    pressToPickUpUI.SetActive(false);
                    Destroy(leekToCollect);
                    doesPlayerHaveLeek = true;
                    leekUIImage.SetActive(true);


                }
            }
        }
    }

    /* GAME AUDIO TIP
    MarketSellerEnd checks if the player has the leeks and will allow the player
    to complete the quest and start the end quest dialogue
    */
    void MarketSellerEnd()
    {
        if (doesPlayerHaveLeek == true && marketSellerTrigger.GetComponent<MarketSellerTrigger>().playerIsInMarketSellerTrigger == true)
        {
            pressToGiveUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                LeekQuestAudio("Quest End");
                LeekQuestAudio("Put Down");
                //Dialogue should be called here

                doesPlayerHaveLeek = false;
                pressToGiveUI.SetActive(false);
                leekToGive.SetActive(true);
                leekUIImage.SetActive(false);
            }
        }
    }

    void FMODInitialiseEvents()
    {
        pickupTransform = leekToCollect.GetComponent<Transform>();
        putdownTransform = leekToGive.GetComponent<Transform>();

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pickupEventInstance, pickupTransform);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(putdownEventInstance, putdownTransform);
    }

    void LeekQuestAudio(string soundToPlay)
    {
        if(soundToPlay == "Quest Start")
        {
            //This is a 2D Sound and doesn't require 3D attributes
            questStartEventInstance = FMODUnity.RuntimeManager.CreateInstance(questStartEvent);
            questStartEventInstance.start();
            questStartEventInstance.release();
        } 
        else if(soundToPlay == "Quest End")
        {
            //This is a 2D Sound and doesn't require 3D attributes
            questEndEventInstance = FMODUnity.RuntimeManager.CreateInstance(questEndEvent);
            questEndEventInstance.start();
            questEndEventInstance.release();

        } 
        else if(soundToPlay == "Pickup")
        {
            pickupEventInstance = FMODUnity.RuntimeManager.CreateInstance(pickupEvent);
            GameObject leekPlayback = new GameObject();
            leekPlayback.transform.position = pickupTransform.position;
            pickupEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(leekPlayback));
            pickupEventInstance.start();
            pickupEventInstance.release();
            Destroy(leekPlayback);
        } 
        else if(soundToPlay == "Put Down")
        {
            putdownEventInstance = FMODUnity.RuntimeManager.CreateInstance(putdownEvent);
            GameObject leekPlayback = new GameObject();
            leekPlayback.transform.position = putdownTransform.position;
            putdownEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(leekPlayback));
            putdownEventInstance.start();
            putdownEventInstance.release();
            Destroy(leekPlayback);
        }
    }


}
