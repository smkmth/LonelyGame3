using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlicker : MonoBehaviour {

    Light candleLight;
    float currentTime;
    public float timeToMove;
    public float lowestVal;
    public float midVal;
    public float highestVal;

    public float lowMoveTime;
    public float highMoveTime;

    private float randomLow;
    private float randomHigh;
    public bool initial =true;
    public bool lightIsOn;

    // Use this for initialization
    void Start () {

        lightIsOn = true;
        candleLight = GetComponent<Light>();
	}
	
    public void ToggleLight(bool lampIsOn)
    {
        if (lampIsOn)
        {
            lightIsOn = false;
            StartCoroutine(LightFade(candleLight, 0.0f, .3f));
        }
        else
        {
            lightIsOn = true;

        }
    }

    IEnumerator LightFade(Light lightToFade, float targetBrightness, float duration)
    {

        float startBrightness = lightToFade.intensity;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float blend =0f;
            if (time > 0)
            {
                blend = time / duration;
            }
            lightToFade.intensity = Mathf.Lerp(startBrightness, targetBrightness, blend);
            yield return null;
        }

    }
    // Update is called once per frame
    void Update () {

        if (lightIsOn)
        {

            if (currentTime <= timeToMove)
            {
            
                currentTime += Time.deltaTime * 10.0f;
                if (initial)
                {
                    candleLight.intensity = Mathf.Lerp(randomLow, randomHigh, currentTime / timeToMove);
                    initial = false;

                }
                else
                {
                    candleLight.intensity = Mathf.Lerp(randomHigh, randomLow, currentTime / timeToMove);
                }

            }
            else
            {
                if (!initial)
                {
                    initial = true;
                    timeToMove = Random.Range(lowMoveTime, randomHigh);
                    randomLow = Random.Range(lowestVal, midVal);
                    randomHigh = Random.Range(midVal, randomHigh);

                }
                candleLight.intensity = midVal;
                currentTime = 0f;
            }
        }
        


    }

}
