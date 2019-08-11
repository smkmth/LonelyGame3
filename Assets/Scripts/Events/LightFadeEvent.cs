using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFadeEvent : GameEventReceiver
{
    public float fadeRate;
    public Light targetLight;
    bool fadingLight =false;

    public override void DoEvent()
    {
        fadingLight = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (fadingLight)
        {

            if (targetLight.intensity > 0)
            {
                targetLight.intensity -= Time.deltaTime * fadeRate;
            }
        }
    }
}
