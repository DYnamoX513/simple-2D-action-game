using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public static EnemySoundManager instance;

    public AudioSource audioSource;

    [SerializeField]
    private AudioClip death, hurt, rage, turtleSpike, bulletFire, bulletCrash, slimeParticle, slimeLarger;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
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

    public void OpenFire()
    {
        audioSource.clip = bulletFire;
        audioSource.Play();
    }

    public void BulletCrash()
    {
        audioSource.clip = bulletCrash;
        audioSource.Play();
    }

    public void SlimeParticle()
    {
        audioSource.clip = slimeParticle;
        audioSource.Play();
    }

    public void SlimeLarger()
    {
        audioSource.clip = slimeLarger;
        audioSource.Play();
    }
}
