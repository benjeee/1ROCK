using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudioManager : MonoBehaviour {

    [SerializeField]
    private AudioClip hurtSound;

    [SerializeField]
    private AudioClip dieSound;

    private AudioSource dieAudioSource;
    private AudioSource hurtAudioSource;

    // Use this for initialization
    void Awake () {
        //audioSource = GetComponent<AudioSource>();
        dieAudioSource = gameObject.AddComponent<AudioSource>();
        hurtAudioSource = gameObject.AddComponent<AudioSource>();
    }
	
	public void playDeathSound()
    {
        dieAudioSource.clip = dieSound;
        dieAudioSource.Play();
    }

    public void playHurtSound()
    {
        if (hurtAudioSource == null)
        {
            Debug.Log("HURTSOURCENULL");
        }
        hurtAudioSource.clip = hurtSound;
        hurtAudioSource.Play();
    }
}
