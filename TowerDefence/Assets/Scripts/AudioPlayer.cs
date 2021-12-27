using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public bool mute = false;

    [SerializeField] private Sound[] music;

    [SerializeField] private Sound[] lazerSounds;
    [SerializeField] private Sound[] missileSounds;
    [SerializeField] private Sound[] electricSounds;
    [SerializeField] private Sound[] hitSounds;
    [SerializeField] private Sound[] explosionSounds;
    [SerializeField] private Sound[] bigExplosionSounds;

    private Sound currentSong;
    private int level = 0;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("audioPlayer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        // Set up sounds
        Sound[] allSounds = lazerSounds.Concat(missileSounds).ToArray();
        allSounds = allSounds.Concat(electricSounds).ToArray();
        allSounds = allSounds.Concat(hitSounds).ToArray();
        allSounds = allSounds.Concat(explosionSounds).ToArray();
        allSounds = allSounds.Concat(bigExplosionSounds).ToArray();

        foreach (Sound sound in allSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = false;
            sound.source.volume = 0.2f;
            sound.source.pitch = 1f;
        }

        foreach (Sound song in music)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.clip = song.clip;
            song.source.loop = true;
            song.source.volume = 0.5f;
            song.source.pitch = 1f;
        }
    }

    private void Start()
    {
        StartMusic();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != level)
        {
            StopMusic();
            level++;
            StartMusic();
        }
    }

    public void StopMusic()
    {
        currentSong.source.Stop();
    }

    public void StartMusic()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSong = music[sceneIndex];
        currentSong.source.Play();
    }

    public void PauseMusic()
    {
        currentSong.source.Pause();
    }

    // Play sound functions

    public void PlayLazerSound()
    {
        if (mute)
        {
            return;
        }
        Sound sound = lazerSounds[Random.Range(0, lazerSounds.Length)];
        sound.source.Play();
    }

    public void PlayMissileSound()
    {
        if (mute)
        {
            return;
        }
        Sound sound = missileSounds[Random.Range(0, missileSounds.Length)];
        sound.source.Play();
    }

    public void PlayElectricSound()
    {
        if (mute)
        {
            return;
        }
        Sound sound = electricSounds[Random.Range(0, electricSounds.Length)];
        sound.source.Play();
    }

    public void PlayHitSound()
    {
        if (mute)
        {
            return;
        }
        Sound sound = hitSounds[Random.Range(0, hitSounds.Length)];
        sound.source.Play();
    }

    public void PlayExplosionSound()
    {
        if (mute)
        {
            return;
        }
        Sound sound = explosionSounds[Random.Range(0, explosionSounds.Length)];
        sound.source.Play();
    }

    public void PlayBigExplosionSound()
    {
        if (mute)
        {
            return;
        }
        Sound sound = bigExplosionSounds[Random.Range(0, bigExplosionSounds.Length)];
        sound.source.Play();
    }
}
