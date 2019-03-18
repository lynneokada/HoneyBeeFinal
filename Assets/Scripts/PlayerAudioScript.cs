using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public AudioSource quickFartSound;
    public AudioSource hawkScreechSound;
    public AudioSource hardCollisionSound;
    public AudioSource boostSound;
    public AudioSource playerHitSound;
    public AudioSource grassHitSound;

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

    public void PlayerHitSound()
    {
        playerHitSound.Play();
    }

    public void GrassHitSound()
    {
        grassHitSound.Play();
    }
}
