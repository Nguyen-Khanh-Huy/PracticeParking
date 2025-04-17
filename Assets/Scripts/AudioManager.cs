using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("AudioSource: ")]
    public AudioSource AusMusic;
    public AudioSource AusSFX;

    [Header("AudioClip MUSIC: ")]
    public AudioClip Music;

    [Header("AudioClip SFX: ")]
    public AudioClip SfxCollect;
    public AudioClip SfxBtnClick;
    public AudioClip SfxHit;

    private void Start()
    {
        PlayMusic(Music);
    }

    public void PlayMusic(AudioClip music)
    {
        AusMusic.clip = music;
        AusMusic.loop = true;
        AusMusic.Play();
    }
    public void PlaySFX(AudioClip sfx)
    {
        AusSFX.clip = sfx;
        AusSFX.loop = false;
        AusSFX.Play();
    }
}