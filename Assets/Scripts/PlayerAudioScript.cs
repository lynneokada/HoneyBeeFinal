using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public AudioSource quickFartSound;
    public AudioSource hawkScreechSound;

    public void PlayQuickFartSound()
    {
        quickFartSound.Play();
    }
    public void PlayHawkScreechSound()
    {
        hawkScreechSound.Play();
    }
}
