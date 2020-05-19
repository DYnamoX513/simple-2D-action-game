using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSourcePerson;
    public AudioClip jumpAudio, hurtAudio, eatAudio,dashAudio,deathAudio;

    private void Awake()
    {
        /*
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }*/

        instance = this;
        DontDestroyOnLoad(this);
    }


    public void JumpAudio()
    {
        audioSourcePerson.clip = jumpAudio;
        audioSourcePerson.Play();
    }

    public void HurtAudio()
    {
        audioSourcePerson.clip = hurtAudio;
        audioSourcePerson.Play();
    }

    public void DashAudio()
    {
        audioSourcePerson.clip = dashAudio;
        audioSourcePerson.Play();
    }

    public void EatAudio()
    {
        audioSourcePerson.clip = eatAudio;
        audioSourcePerson.Play();
    }

    public void DeathAudio()
    {
        audioSourcePerson.clip = deathAudio;
        audioSourcePerson.Play();
    }
}
