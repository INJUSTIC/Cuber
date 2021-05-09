using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicAndEffectsIconsButtons : MonoBehaviour
{
    public Button MusicIconButton;
    public Button EffectsIconButton;
    public Sprite MusicIcon;
    public Sprite MusicIconCrossedOut;
    public Sprite EffectsIcon;
    public Sprite EffectsIconCrossedOut;
    public Sprite WhiteMusicIcon;
    public Sprite WhiteMusicIconCrossedOut;
    public Sprite WhiteEffectsIcon;
    public Sprite WhiteEffectsIconCrossedOut;
    private Color LandColor;
    void Start()
    {
        ColorData cl = SaveSystem.LoadColorofLand();
        LandColor = new Color32(cl.Color[0], cl.Color[1], cl.Color[2], 255);
        AudioManager.MusicState = SaveSystem.LoadMusicToggle();
        AudioManager.EffectsState = SaveSystem.LoadEffectsToggle();
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (AudioManager.EffectsState)
            {
                EffectsIconButton.image.sprite = EffectsIcon;
            }
            else
            {
                EffectsIconButton.image.sprite = EffectsIconCrossedOut;
            }
            if (AudioManager.MusicState)
            {
                MusicIconButton.image.sprite = MusicIcon;
            }
            else
            {
                MusicIconButton.image.sprite = MusicIconCrossedOut;
            }
        }
        else
        {
            if (LandColor == Color.black)
            {
                if (AudioManager.EffectsState)
                {
                    EffectsIconButton.image.sprite = WhiteEffectsIcon;
                }
                else
                {
                    EffectsIconButton.image.sprite = WhiteEffectsIconCrossedOut;
                }
                if (AudioManager.MusicState)
                {
                    MusicIconButton.image.sprite = WhiteMusicIcon;
                }
                else
                {
                    MusicIconButton.image.sprite = WhiteMusicIconCrossedOut;
                }
            }
            else
            {
                if (AudioManager.MusicState)
                {
                    MusicIconButton.image.sprite = MusicIcon;
                }
                else
                {
                    MusicIconButton.image.sprite = MusicIconCrossedOut;
                }
                if (AudioManager.EffectsState)
                {
                    EffectsIconButton.image.sprite = EffectsIcon;
                }
                else
                {
                    EffectsIconButton.image.sprite = EffectsIconCrossedOut;
                }
            }
        }
    }
    public void OnMusicButtonClicked()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (MusicIconButton.image.sprite == MusicIcon)
            {
                AudioManager.MusicState = false;
                SaveSystem.SaveMusicToggle(false);
                MusicIconButton.image.sprite = MusicIconCrossedOut;
                foreach (Sound sound in FindObjectOfType<AudioManager>().sounds)
                {
                    sound.source.volume = 0;
                }
            }
            else
            {
                AudioManager.MusicState = true;
                SaveSystem.SaveMusicToggle(true);
                MusicIconButton.image.sprite = MusicIcon;
                foreach (Sound sound in FindObjectOfType<AudioManager>().sounds)
                {
                    sound.source.volume = 1;
                }
            }
        }
        else
        {
            if (LandColor == Color.black)
            {
                if (MusicIconButton.image.sprite == WhiteMusicIcon)
                {
                    AudioManager.MusicState = false;
                    SaveSystem.SaveMusicToggle(false);
                    MusicIconButton.image.sprite = WhiteMusicIconCrossedOut;
                    foreach (Sound sound in FindObjectOfType<AudioManager>().sounds)
                    {
                        sound.source.volume = 0;
                    }
                }
                else
                {
                    AudioManager.MusicState = true;
                    SaveSystem.SaveMusicToggle(true);
                    MusicIconButton.image.sprite = WhiteMusicIcon;
                    foreach (Sound sound in FindObjectOfType<AudioManager>().sounds)
                    {
                        sound.source.volume = 1;
                    }
                }
            }
            else
            {
                if (MusicIconButton.image.sprite == MusicIcon)
                {
                    AudioManager.MusicState = false;
                    SaveSystem.SaveMusicToggle(false);
                    MusicIconButton.image.sprite = MusicIconCrossedOut;
                    foreach (Sound sound in FindObjectOfType<AudioManager>().sounds)
                    {
                        sound.source.volume = 0;
                    }
                }
                else
                {
                    AudioManager.MusicState = true;
                    SaveSystem.SaveMusicToggle(true);
                    MusicIconButton.image.sprite = MusicIcon;
                    foreach (Sound sound in FindObjectOfType<AudioManager>().sounds)
                    {
                        sound.source.volume = 1;
                    }
                }
            }
        }
        /*if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (!AudioManager.MusicState) FindObjectOfType<AudioManager>().Stop("Theme");
            else FindObjectOfType<AudioManager>().Play("Theme");
        }
        else if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            if (!AudioManager.MusicState) FindObjectOfType<AudioManager>().Stop(AudioManager.CurrentMusic);
            else FindObjectOfType<AudioManager>().Play(AudioManager.CurrentMusic);
        }
        else
        {
            if (!AudioManager.MusicState) FindObjectOfType<AudioManager>().Stop("MusicForLevels");
            else FindObjectOfType<AudioManager>().Play("MusicForLevels");
        }*/
    }
    public void OnEffectsButtonClicked()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (EffectsIconButton.image.sprite == EffectsIcon)
            {
                AudioManager.EffectsState = false;
                SaveSystem.SaveEffectsToggle(false);
                EffectsIconButton.image.sprite = EffectsIconCrossedOut;
            }
            else
            {
                AudioManager.EffectsState = true;
                SaveSystem.SaveEffectsToggle(true);
                EffectsIconButton.image.sprite = EffectsIcon;
            }
        }
        else
        {
            if (LandColor == Color.black)
            {
                if (EffectsIconButton.image.sprite == WhiteEffectsIcon)
                {
                    AudioManager.EffectsState = false;
                    SaveSystem.SaveEffectsToggle(false);
                    EffectsIconButton.image.sprite = WhiteEffectsIconCrossedOut;
                }
                else
                {
                    AudioManager.EffectsState = true;
                    SaveSystem.SaveEffectsToggle(true);
                    EffectsIconButton.image.sprite = WhiteEffectsIcon;
                }
            }
            else
            {
                if (EffectsIconButton.image.sprite == EffectsIcon)
                {
                    AudioManager.EffectsState = false;
                    SaveSystem.SaveEffectsToggle(false);
                    EffectsIconButton.image.sprite = EffectsIconCrossedOut;
                }
                else
                {
                    AudioManager.EffectsState = true;
                    SaveSystem.SaveEffectsToggle(true);
                    EffectsIconButton.image.sprite = EffectsIcon;
                }
            }
        }
    }
}
