using UnityEngine;
using System.Collections;
public class SetHRTF : MonoBehaviour
{
    public enum ROOMSIZE { Small, Medium, Large, None };
    public ROOMSIZE room = ROOMSIZE.Small;  // Small is regarded as the "most average"
                                            // defaults and docs from MSDN
                                            // https://msdn.microsoft.com/library/windows/desktop/mt186602(v=vs.85).aspx
    AudioSource audiosource;
    void Awake()
    {
        audiosource = this.gameObject.GetComponent<AudioSource>();
        if (audiosource == null)
        {
            print("SetHRTFParams needs an audio source to do anything.");
            return;
        }
        audiosource.spatialize = true; // we DO want spatialized audio
        audiosource.spread = 0; // we dont want to reduce our angle of hearing
        audiosource.spatialBlend = 1;   // we do want to hear spatialized audio
        audiosource.SetSpatializerFloat(1, (float)room);    // 1 is the roomsize param
    }
}