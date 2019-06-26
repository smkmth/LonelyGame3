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
    public ScriptableEvent eventToTrigger;
    public AudioSource audioSource;
    public GameObject animatedObject;
    public float holdTimer;
    public PickUpItem player;
    public Ghost ghost;
    public GameObject winGameImage;

    public void Start()
    {

        player = GameObject.Find("Player").GetComponent<PickUpItem>();
       
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
        if (!hasBeenTriggered || canTriggerAgain)
        {
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
                        winGameImage.SetActive(true);

                    }
                    break;

            
            }
            hasBeenTriggered = true;
        }
    }


}
