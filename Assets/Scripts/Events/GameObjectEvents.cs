using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameObjectEventType
{
    ActivateObject,
    DeactivateObject,
    TeleportObject,
    ScaleObject,
    RotateObject,
}
[AddComponentMenu("GameEvents/GameObject Event")]
public class GameObjectEvents : GameEventReceiver
{
    public GameObject gameObjectToEffect;
    public GameObjectEventType eventType;
    public Transform teleportTarget;
    public Vector3 scaleTarget;

    public override void DoEvent()
    {
        switch (eventType)
        {
            case GameObjectEventType.ActivateObject:
                gameObjectToEffect.SetActive(true);
                break;
            case GameObjectEventType.DeactivateObject:
                gameObjectToEffect.SetActive(false);
                break;
            case GameObjectEventType.TeleportObject:
                gameObjectToEffect.transform.position = teleportTarget.position;
                break;

            case GameObjectEventType.RotateObject:
                gameObjectToEffect.transform.rotation = teleportTarget.rotation;
                break;
            case GameObjectEventType.ScaleObject:
                gameObjectToEffect.transform.localScale = scaleTarget;
                break;


        }
    }
}
