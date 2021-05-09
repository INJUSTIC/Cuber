using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private bool VolumeIsLawing = false;
    public static bool MusicState = true;
    public static bool EffectsState = true;
    public static int CurrentMusic;
    private AudioSource _AudioSource;
    private float[] _samples = new float[512];
    public static float SpectrumValue;
    private StartGame _StartGame;
    private void Awake()
    {
        _StartGame = FindObjectOfType<StartGame>();
        MusicState = SaveSystem.LoadMusicToggle();
        EffectsState = SaveSystem.LoadEffectsToggle();
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.name = sound.name;
            if (MusicState)
            {
                // if(SceneManager.GetActiveScene().name == "UnlimitedLevel" || SceneManager.GetActiveScene().name == "Main Menu")
                sound.source.volume = 1f;
                //  else sound.source.volume = 0.6f;
            }
            else sound.source.volume = 0;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            CurrentMusic = UnityEngine.Random.Range(0, FindObjectOfType<AudioManager>().sounds.Length);
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Play("Theme");
        }
        //Play(5);
    }
    private void Update()
    {
        /*if (SceneManager.GetActiveScene().name == "UnlimitedLevel" && MusicState && !_StartGame.IsFirst && !sounds[CurrentMusic].source.isPlaying && Time.timeScale != 0)
        {
            int currentmusic = UnityEngine.Random.Range(0, sounds.Length - 1);
            while (currentmusic == CurrentMusic)
            {
                currentmusic = UnityEngine.Random.Range(0, sounds.Length - 1);
            }
            CurrentMusic = currentmusic;
            Play(CurrentMusic);
        }
        if (_AudioSource != null && _samples != null && _samples.Length > 0)
        {
            SpectrumValue = _samples[0] * 100;
            _AudioSource.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
        }
        if (FinishGame.IsCompleted && SceneManager.GetActiveScene().name != "UnlimitedLevel" && SceneManager.GetActiveScene().name != "Main Menu" && MusicState && !VolumeIsLawing)
        {
            VolumeIsLawing = true;
            float Amount = sounds[CurrentMusic].volume / 40;
            StartCoroutine(LowMusic(Amount));
        }*/

    }
    IEnumerator LowMusic(float Amount)
    {
        while (sounds[CurrentMusic].source.volume != 0)
        {
            sounds[CurrentMusic].source.volume -= Amount;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null) Debug.Log($"There is no Clip with {name} name");
        else
        {
            s.source.Play();
            _AudioSource = s.source;
        }
    }
    public void Play(int index)
    {
        if (index >= 0 && index < sounds.Length)
        {
            sounds[index].source.Play();
            _AudioSource = sounds[index].source;
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null) Debug.Log($"There is no Clip with {name} name");
        else s.source.Stop();
    }
    public void Stop(int index)
    {
        if (index >= 0 && index < sounds.Length)
        {
            sounds[index].source.Stop();
        }
    }
    public void Pause(int index)
    {
        if (index >= 0 && index < sounds.Length)
        {
            sounds[index].source.Pause();
        }
    }
    public void Resume(int index)
    {
        if (index >= 0 && index < sounds.Length)
        {
            sounds[index].source.UnPause();
        }
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null) Debug.Log($"There is no Clip with {name} name");
        else s.source.Pause();
    }
    public void Resume(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null) Debug.Log($"There is no Clip with {name} name");
        else s.source.UnPause();
    }
    public void Restart(string name)
    {
        Stop(name);
        Play(name);
    }
}
