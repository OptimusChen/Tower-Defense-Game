using System;
using System.Collections.Generic;
using UnityEngine;


public class Oneshotter : MonoBehaviour
{
    [Serializable]
    public class SoundCollection
    {
        public List<AudioClip> liAudioClips = new List<AudioClip>();

        public AudioClip AudioClipSelectRandom()
        {
            return this.liAudioClips[UnityEngine.Random.Range(0, this.liAudioClips.Count)];
        }
    }

    public static Oneshotter singleton;

    private AudioSource audioSource;

    [SerializeField]
    private Oneshotter.SoundCollection scLaser;

    [SerializeField]
    private Oneshotter.SoundCollection scEnemyKill;

    [SerializeField]
    private Oneshotter.SoundCollection scHover;

    [SerializeField]
    private Oneshotter.SoundCollection scOvercharge;

    [SerializeField]
    private Oneshotter.SoundCollection scTurretDestroy;

    [SerializeField]
    private Oneshotter.SoundCollection scWin;

    [SerializeField]
    private Oneshotter.SoundCollection scLose;

    [SerializeField]
    private Oneshotter.SoundCollection scEnemyGotThrough;

    [SerializeField]
    private Oneshotter.SoundCollection scSplashAttack;

    [SerializeField]
    private Oneshotter.SoundCollection scSnipeAttack;

    [SerializeField]
    private Oneshotter.SoundCollection scBounceHurt;

    private void Awake()
    {
        Oneshotter.singleton = this;
    }

    private void Start()
    {
        this.audioSource = base.GetComponent<AudioSource>();
    }

    public void PlayLaserSound(bool _bOvercharged)
    {
        if (_bOvercharged)
        {
            this.audioSource.PlayOneShot(this.scLaser.AudioClipSelectRandom(), 2.5f);
        }
        else
        {
            this.audioSource.PlayOneShot(this.scLaser.AudioClipSelectRandom(), 1f);
        }
    }

    public void PlayEnemyKillSound()
    {
        this.audioSource.PlayOneShot(this.scEnemyKill.AudioClipSelectRandom(), 0.5f);
    }

    public void PlayHoverSound()
    {
        this.audioSource.PlayOneShot(this.scHover.AudioClipSelectRandom(), 1f);
    }

    public void PlayOverchargeSound()
    {
        this.audioSource.PlayOneShot(this.scOvercharge.AudioClipSelectRandom(), 1f);
    }

    public void PlayTurretDestroySound()
    {
        this.audioSource.PlayOneShot(this.scTurretDestroy.AudioClipSelectRandom(), 2f);
    }

    public void PlayWinSound()
    {
        this.audioSource.PlayOneShot(this.scWin.AudioClipSelectRandom(), 2f);
    }

    public void PlayLoseSound()
    {
        this.audioSource.PlayOneShot(this.scLose.AudioClipSelectRandom(), 2f);
    }

    public void PlayEnemyGotThroughSound()
    {
        this.audioSource.PlayOneShot(this.scEnemyGotThrough.AudioClipSelectRandom(), 2f);
    }

    public void PlaySplashAttackSound(bool _bOvercharged)
    {
        if (_bOvercharged)
        {
            this.audioSource.PlayOneShot(this.scSplashAttack.AudioClipSelectRandom(), 1f);
        }
        else
        {
            this.audioSource.PlayOneShot(this.scSplashAttack.AudioClipSelectRandom(), 0.25f);
        }
    }

    public void PlaySnipeAttackSound(bool _bOvercharged)
    {
        if (_bOvercharged)
        {
            this.audioSource.PlayOneShot(this.scSnipeAttack.AudioClipSelectRandom(), 0.7f);
        }
        else
        {
            this.audioSource.PlayOneShot(this.scSnipeAttack.AudioClipSelectRandom(), 0.25f);
        }
    }

    public void PlayBounceHurtSound(bool _bOvercharged)
    {
        if (_bOvercharged)
        {
            this.audioSource.PlayOneShot(this.scBounceHurt.AudioClipSelectRandom(), 2f);
        }
        else
        {
            this.audioSource.PlayOneShot(this.scBounceHurt.AudioClipSelectRandom(), 0.8f);
        }
    }
}