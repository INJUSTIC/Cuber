using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Image CoinImage;
    public TextMeshProUGUI CoinNumber;
    public GameObject PausePanel;
    public Button MusicIconButton;
    public Button EffectIconButton;
    public Sprite MusicIcon;
    public Sprite EffectsIcon;
    public Sprite MusicIconCrossedOut;
    public Sprite EffectsIconCrossedOut;
    //Это для разных бэкграундов
    /*public Sprite WhiteMusicIcon;
    public Sprite WhiteEffectsIcon;
    public Sprite WhiteMusicIconCrossedOut;
    public Sprite WhiteEffectsIconCrossedOut;*/

    private void Start()
    {
        //Это для разных бэкграундов
        /*ColorData cl = SaveSystem.LoadColorofLand();
        Color LandColor = new Color32(cl.Color[0], cl.Color[1], cl.Color[2], 255);
        if (LandColor == Color.black)
        {
            if (AudioManager.EffectsState)
            {
                EffectIconButton.image.sprite = WhiteEffectsIcon;
            }
            else
            {
                EffectIconButton.image.sprite = WhiteEffectsIconCrossedOut;
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
        {*/

        //}
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        transform.Find("Pause").gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel" && CoinImage.gameObject.activeSelf)
        {
            CoinImage.enabled = false;
            CoinNumber.enabled = false;
        }
        FindObjectOfType<AudioManager>().Pause(AudioManager.CurrentMusic);
        PausePanel.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        transform.Find("Pause").gameObject.SetActive(true);
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel" && CoinImage.gameObject.activeSelf)
        {
            CoinImage.enabled = true;
            CoinNumber.enabled = true;
        }
        FindObjectOfType<AudioManager>().Resume(AudioManager.CurrentMusic);
        PausePanel.SetActive(false);
    }
    public void Quit()
    {
        Camera.main.GetComponent<FastMobileBloom>().enabled = false;
        Camera.main.GetComponent<FollowPlayer>().enabled = false;
        FindObjectOfType<SceneLoader>().LoadScene(0);
        transform.Find("Panel").gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            FindObjectOfType<Score>().GetComponent<TextMeshProUGUI>().enabled = false;
        }
        //Time.timeScale = 1f;
    }
    private void OnApplicationPause(bool pause)
    {
        if (SceneManager.GetActiveScene().name != "UnlimitedLevel")
        {
            if (pause && !FindObjectOfType<Restart>().RestartPanel.activeSelf
            && !FindObjectOfType<StartGame>().StartPanel.activeSelf
            && !FindObjectOfType<LoadNewGame>().Panel.activeSelf
            && !FindObjectOfType<LoadNewGame>().NextPanel.activeSelf
            && !ScriptCollision.IsOvered)
            {
                Pause();
            }
        }
        else
        {
            if (pause && !ScriptCollision.IsOvered && !FindObjectOfType<Restart>().RestartPanel.activeSelf
           && !FindObjectOfType<StartGame>().StartPanel.activeSelf && FindObjectOfType<ContinuePlayAfterAd>().TimerText.text == "")
            {
                Pause();
            }
        }
    }
}


