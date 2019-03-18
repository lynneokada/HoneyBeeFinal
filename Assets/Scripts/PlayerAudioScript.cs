using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public AudioSource quickFartSound;
    public AudioSource hawkScreechSound;
    public AudioSource hardCollisionSound;
    public AudioSource boostSound;

    public void PlayQuickFartSound()
    {
        quickFartSound.Play();
    }
    public void PlayHawkScreechSound()
    {
        hawkScreechSound.Play();
    }

    public void PlayHardCollisionSound()
    {
        hardCollisionSound.Play();
    }

    public void PlayBoostSound()
    {
        boostSound.Play();
    }
}
