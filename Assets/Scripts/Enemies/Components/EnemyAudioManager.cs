using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudioManager : MonoBehaviour {

    [SerializeField]
    private AudioClip hurtSound;

    [SerializeField]
    private AudioClip dieSound;

    [SerializeField]
    private AudioSource audioSource;
	
	public void playDeathSound()
    {
        audioSource.PlayOneShot(dieSound, 1);
    }

    public void playHurtSound()
    {
        audioSource.PlayOneShot(hurtSound, 1);
    }
}
