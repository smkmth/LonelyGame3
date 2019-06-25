using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetector : MonoBehaviour
{

    public float spaceBetweenTicks;
    public float timer;
    public AudioClip[] gigerClicks;
    public AudioClip[] urgentGigerClicks;
    public Transform Ghost;
    private AudioSource audioSource;
    public float startDist;
    public float dist;
    public void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        dist = Mathf.Abs(Vector3.Distance(transform.position, Ghost.position));


        spaceBetweenTicks = dist*dist * 0.01f;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spaceBetweenTicks;
            HelperFunctions.PlayRandomNoiseInArray(gigerClicks, audioSource, .5f);
        }



    }
}
