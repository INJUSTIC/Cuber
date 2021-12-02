using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lean.Localization;

public class Settings : MonoBehaviour
{
    public LeanLocalization lean;
    public Button[] language_buttons;
    public const float ChosedSize = 2.62f;
    public const float UnChosedSize = 1.93f;
    public void Start()
    {
        SetLanguage(PlayerPrefs.GetInt("Language"));
        if (PlayerPrefs.GetInt("Language") != 0)
        {
            language_buttons[PlayerPrefs.GetInt("Language") - 1].transform.localScale = new Vector2(ChosedSize, ChosedSize);
        }
        else
        {
            switch(Application.systemLanguage.ToString())
            {
                
                case "Russian":
                    {
                        language_buttons[1].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 2);
                        break;
                    }
                case "German":
                    {
                        language_buttons[2].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 3);
                        break;
                    }
                case "Spanish":
                    {
                        language_buttons[3].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 4);
                        break;
                    }
                case "Italian":
                    {
                        language_buttons[4].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 5);
                        break;
                    }
                case "French":
                    {
                        language_buttons[5].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 6);
                        break;
                    }
                case "Chinese":
                    {
                        language_buttons[6].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 7);
                        break;
                    }
                case "Arabic":
                    {
                        language_buttons[7].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 8);
                        break;
                    }
                case "English":
                default:
                    {
                        language_buttons[0].transform.localScale = new Vector2(ChosedSize, ChosedSize);
                        PlayerPrefs.SetInt("Language", 1);
                        break;
                    }
            }
        }
    }
    public void OnLanguageButtonClicked(Button button)
    {
        if(PlayerPrefs.GetInt("Language")!= 0)
        {
            language_buttons[PlayerPrefs.GetInt("Language") - 1].transform.localScale = new Vector2(UnChosedSize, UnChosedSize);
        }
        button.transform.localScale = new Vector2(ChosedSize, ChosedSize);
        for(int i = 0; i < language_buttons.Length; i++)
        {
            if (language_buttons[i] == button)
            {
                PlayerPrefs.SetInt("Language", i+1);//i+1 чтобы не было 0, потому что при передаче параметром language_id в SetLanguage
                                                   //должен передаваться 0 только при условии, что мы заходим в 1 раз в игру
                break;
            }
        }
    }

    public static void SetLanguage(int language_id)
    {
        LeanLocalization lean = FindObjectOfType<LeanLocalization>();
        switch (language_id)
        {
            case 1:
                {
                    lean.SetCurrentLanguage("English");
                    break;
                }
            case 2:
                {
                    lean.SetCurrentLanguage("Russian");
                    break;
                }
            case 3:
                {
                    lean.SetCurrentLanguage("German");
                    break;
                }
            case 4:
                {
                    lean.SetCurrentLanguage("Spain");
                    break;
                }
            case 5:
                {
                    lean.SetCurrentLanguage("Italian");
                    break;
                }
            case 6:
                {
                    lean.SetCurrentLanguage("French");
                    break;
                }
            case 7:
                {
                    lean.SetCurrentLanguage("Chinese");
                    break;
                }
            case 8:
                {
                    lean.SetCurrentLanguage("Arabic");
                    break;
                }
            default:
                {
                    string system_lang = Application.systemLanguage.ToString();
                    if (system_lang == "English" || system_lang == "Russian" || system_lang == "German" || system_lang == "Spain" ||
                        system_lang == "Italian" || system_lang == "French" || system_lang == "Chinese" || system_lang == "Arabic")
                    {
                        lean.SetCurrentLanguage(Application.systemLanguage.ToString());
                    }
                    else
                    {
                        lean.SetCurrentLanguage("English");
                    }
                    break;
                }
        }
    }



    //наверх
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
}
