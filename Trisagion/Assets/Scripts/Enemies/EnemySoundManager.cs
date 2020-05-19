using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public static EnemySoundManager instance;

    public AudioSource audioSource;

    [SerializeField]
    private AudioClip death, hurt, rage, turtleSpike;
    private void Awake()
    {
        instance = this;
    }

    public void Death()
    {
        audioSource.clip = death;
        audioSource.Play();
    }

    public void Hurt()
    {
        audioSource.clip = hurt;
        audioSource.Play();
    }

    public void Rage()
    {
        audioSource.clip = rage;
        audioSource.Play();
    }

    public void Spike()
    {
        audioSource.clip = turtleSpike;
        audioSource.Play();
    }
}
