using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions : MonoBehaviour  {

    public static HelperFunctions Helper;
    public Dictionary<int, int> helperIdToIndex = new Dictionary<int, int>();

    void Start()
    {
        if (!Helper)
        {
            Debug.Log("Created");
            Helper = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// this function plays a random sound in an array, and checks to make sure that the sound was not 
    /// just played recently. Takes an array of audioclips, an audiosource, a volume to play at, a 
    /// unique helperid to that object and optionaly a bool if you want to be told what noise just played
    /// </summary>
    /// <param name="array"></param>
    /// <param name="audioSource"></param>
    /// <param name="vol"></param>
    /// <param name="helperId"></param>
    /// <param name="reportNoisePlayed"></param>
    public void PlayRandomNoiseInArray(AudioClip[] array, AudioSource audioSource, float vol, int helperId,  bool reportNoisePlayed = false)
    {
        if (array.Length > 0)
        {

            if (!audioSource.isPlaying)
            {
                int noiseIndex = CreateIndex(helperId, array.Length);
                AudioClip selectedNoise = array[noiseIndex];
                if (reportNoisePlayed)
                {
                    Debug.Log("Noise Played = " + array[noiseIndex].name);

                }

                audioSource.PlayOneShot(selectedNoise, vol);
            }

        }
        else
        {
            Debug.Log("No noises set for " + audioSource.gameObject.name);
        }

    }

    int CreateIndex(int helperId ,int arrayLength)
    {
        int noiseIndex = Random.Range(0, arrayLength);
        
        if (arrayLength <= 1)
        {
            return noiseIndex;
        }
        
        int emergancyInt = 0;
        if (helperIdToIndex.ContainsKey(helperId))
        {
          
            
            while (helperIdToIndex[helperId] == noiseIndex)
            {
                if (emergancyInt > 200)
                {
                    Debug.Log( " Sound Loop iterated 200 times " );
                    return noiseIndex;

                }
                else
                {
                    emergancyInt++;
                }
                noiseIndex = Random.Range(0, arrayLength);

            }
            helperIdToIndex[helperId] = noiseIndex;
            return noiseIndex;
        }
        else
        {
            helperIdToIndex.Add(helperId, noiseIndex);
            return noiseIndex;
        }

    }
}
