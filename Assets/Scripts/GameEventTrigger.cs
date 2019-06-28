using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TriggerType
{
    OnEnterTriggerBox,
    OnExitTriggerBox,
    Interact,
    HoldInteract
}
public class GameEventTrigger : MonoBehaviour {

    public TriggerType howEventIsTriggered;
    public bool canTriggerAgain =false;
    public bool hasBeenTriggered = false;
    public bool multipleEvents;

    public ScriptableEvent eventToTrigger;
    public ScriptableEvent[] eventsToTrigger;
    public AudioSource audioSource;
    public GameObject animatedObject;
    public float holdTimer;
    public PlayerInteract player;
    public Ghost ghost;
    public GameObject winGameImage;

    public void Start()
    {

        player = GameObject.Find("Player").GetComponent<PlayerInteract>();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (howEventIsTriggered == TriggerType.OnEnterTriggerBox)
            {
                TriggerEvent();
            }


        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (howEventIsTriggered == TriggerType.OnExitTriggerBox)
            {
                TriggerEvent();

            }

        }

    }

    public void TriggerEvent()
    {
        if (!multipleEvents)
        {

            if (!hasBeenTriggered || canTriggerAgain)
                switch (eventToTrigger.eventType)
                {
                    case EventType.Sound:
                        SoundEvent soundEvent = (SoundEvent)eventToTrigger;
                        audioSource.PlayOneShot(soundEvent.audioClip, soundEvent.volumeSetting);
                        break;
                    case EventType.Animation:
                        AnimationEvent animationEvent = (AnimationEvent)eventToTrigger;
                        animatedObject.GetComponent<Animator>().SetTrigger(animationEvent.parameterToTrigger);
                        break;
                    case EventType.Debug:
                        Debug.Log("Event Triggered");
                        break;
                    case EventType.GhostEvent:
                        GhostEvent ghostEvent = (GhostEvent)eventToTrigger;
                        switch (ghostEvent.thisGhostEvent)
                        {
                            case GhostEventType.ActivateGhost:
                                ghost.OnFirstActivateGhost();
                                break;
                        }
                        break;
                    case EventType.GameWinEvent:
                        if (player.isHoldingEndGameItem)
                        {
                            Cursor.visible = true;
                            Cursor.lockState = CursorLockMode.None;

                            winGameImage.SetActive(true);

                        }
                        break;


                }
            hasBeenTriggered = true;

        }
        else
        {
            foreach (ScriptableEvent gameevent in eventsToTrigger)
            {
                if (!hasBeenTriggered || canTriggerAgain)
                    switch (gameevent.eventType)
                    {
                        case EventType.Sound:
                            SoundEvent soundEvent = (SoundEvent)gameevent;
                            audioSource.PlayOneShot(soundEvent.audioClip, soundEvent.volumeSetting);
                            break;
                        case EventType.Animation:
                            AnimationEvent animationEvent = (AnimationEvent)gameevent;
                            animatedObject.GetComponent<Animator>().SetTrigger(animationEvent.parameterToTrigger);
                            hasBeenTriggered = true;
                            break;
                        case EventType.Debug:
                            Debug.Log("Event Triggered");
                            break;
                        case EventType.GhostEvent:
                            GhostEvent ghostEvent = (GhostEvent)gameevent;
                            switch (ghostEvent.thisGhostEvent)
                            {
                                case GhostEventType.ActivateGhost:
                                    ghost.OnFirstActivateGhost();
                                    break;
                            }
                            break;
                        case EventType.GameWinEvent:
                            if (player.isHoldingEndGameItem)
                            {

                                Cursor.lockState = CursorLockMode.None;

                                winGameImage.SetActive(true);

                            }
                            break;


                    }
            }

        }
    }


}
