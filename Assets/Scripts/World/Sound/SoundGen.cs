using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGen : MonoBehaviour {


    public AudioClip[] noises;
    public AudioSource[] sources;
    public float chanceOfNoise;
    public float timeBetweenNoises;
    private float houseTimer;
    private int previousIndex;
    public int helperId = 3;


    // Update is called once per frame
    void Update () {

        if (houseTimer <= 0)
        {
            houseTimer = timeBetweenNoises;
            if (Random.Range(0,100) < chanceOfNoise)
            {
                int sourceindex = Random.Range(0, sources.Length);

                HelperFunctions.Helper.PlayRandomNoiseInArray(noises, sources[sourceindex], Random.Range(0.5f, 1f), helperId);
            }
        }
        else
        {
            houseTimer -= Time.deltaTime;
        }
    }


}
