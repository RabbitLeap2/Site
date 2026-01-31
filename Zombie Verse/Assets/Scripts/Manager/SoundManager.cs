using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    public AudioSource ShootingChannel;
    public AudioSource reloadSoundPistola;
    public AudioSource emptyMagSoundPistola;
    public AudioClip SmgTiro;
    public AudioClip PistolaTiro;

    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;
    public AudioSource ZombieSound;
    public AudioSource ZombieSound2;

    public AudioSource playerChannel;
    public AudioClip PlayerHurt;
    public AudioClip Playerdead;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlayShootingSound(weaponModel weapon)
    {
        switch (weapon)
        {
            case weaponModel.Pistola:
                ShootingChannel.PlayOneShot(PistolaTiro);
                break;
            case weaponModel.Smg:
                ShootingChannel.PlayOneShot(SmgTiro);
                break;
        }
    }

    public void PlayReloadingSound(weaponModel weapon)
    {
        switch (weapon)
        {
            case weaponModel.Pistola:
                reloadSoundPistola.Play();
                break;
            case weaponModel.Smg:
                reloadSoundPistola.Play();

                break;
        }
    }

} 