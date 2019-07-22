using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbySoundGen : MonoBehaviour
{
    public Transform player;
    public float soundRad;
    public float chanceOfNoise;
    public float noisesPerSecond;
    private float noiseTimer;
    public float minDist;
    public LayerMask selfLayer;
    public AudioClip[] noises;
    public AudioSource[] sources;
    public int helperId = 1;
    public bool DebugModeOn;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (noiseTimer <= 0)
        {
            noiseTimer = noisesPerSecond;
            Collider[] hits = Physics.OverlapSphere(player.position, soundRad);

            foreach (Collider hit in hits)
            {
                if (hit.tag == "SoundSource")
                {
                    if (DebugModeOn)
                    {

                        Debug.Log("startedSearch");
                    }

                    

                    RaycastHit potplayer;
                    if (!Physics.Linecast(hit.transform.position, player.position, out potplayer, selfLayer, QueryTriggerInteraction.Ignore))
                    {
                        if (DebugModeOn)
                        {
                            Debug.Log("Hit Nothing");
                        }
                        return;
                    }
                    
                    if (potplayer.collider.tag == "Player")
                    {
                        if (DebugModeOn)
                        {
                            Debug.Log("Hit Player!");
                            Debug.DrawRay(hit.transform.position, (player.position - hit.transform.position), Color.black, 1.0f);
                        }
                    }
                    else
                    {
                        if (Random.Range(0, 100) < chanceOfNoise)
                        {

                            Debug.DrawRay(hit.transform.position, (player.position - hit.transform.position), Color.red, 1.0f);
                            if (Vector3.Distance(hit.transform.position, player.position) > minDist)
                            {
                                if (DebugModeOn)
                                {

                                    Debug.Log("Play" + hit.transform.name);
                                    Debug.DrawRay(hit.transform.position, (player.position - hit.transform.position), Color.blue, 2.0f);
                                }

                                HelperFunctions.Helper.PlayRandomNoiseInArray(noises, hit.GetComponent<AudioSource>(), Random.Range(0.5f, 1f), helperId, false);

                            }
                        }
                    }
                        

                    
                }

            }
            int sourceindex = Random.Range(0, sources.Length);
            
        }
        else
        {
            noiseTimer -= Time.deltaTime;
        }
    }
}
