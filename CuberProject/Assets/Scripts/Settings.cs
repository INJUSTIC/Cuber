using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    /*public Button MusicIconButton;
    public Button EffectsIconButton;
    public Sprite MusicIcon;
    public Sprite MusicIconCrossedOut;
    public Sprite EffectsIcon;
    public Sprite EffectsIconCrossedOut;
     public Sprite MusicIconForBlackBack;
     public Sprite MusicIconForBlackBackCrossedOut;
     public Sprite EffectsIconForBlackBack;
     public Sprite EffectsIconForBlackBackCrossedOut;*/
    /*public void ChangeVolume(float Volume)
    {
        volume = Volume;
        SaveSystem.SaveVolume(volume);
    }*/
    /*private void Start()
    {
        AudioManager.MusicState = SaveSystem.LoadMusicToggle();
        AudioManager.EffectsState = SaveSystem.LoadEffectsToggle();
        // volume = SaveSystem.LoadVolume();
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
        //slider.value = volume;
    }
    public void OnMusicButtonClicked()
    {
        if (MusicIconButton.image.sprite == MusicIcon)
        {
            AudioManager.MusicState = false;
            SaveSystem.SaveMusicToggle(false);
            MusicIconButton.image.sprite = MusicIconCrossedOut;
        }
        else
        {
            AudioManager.MusicState = true;
            SaveSystem.SaveMusicToggle(true);
            MusicIconButton.image.sprite = MusicIcon;
        }
        if (SceneManager.GetActiveScene().name == "Main Menu")
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
        }
    }
    public void OnEffectsButtonClicked()
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
    }*/
    public void OnLanguageClicked(Button button)
    {
        button.transform.localScale = new Vector2(2.623f, 2.623f);
    }
}
