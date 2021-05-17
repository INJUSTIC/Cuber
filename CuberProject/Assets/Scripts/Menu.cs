using System.Collections;
using Firebase;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using System.Diagnostics;
using TMPro;
using Google.Play.Review;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject ChooseModePanel;
    public GameObject ChooseLevelPanel;
    public GameObject LeaderboardPanel;
    public GameObject ShopPanel;
    public GameObject AchievmentsPanel;
    public GameObject CreditsPanel;
    public GameObject Cube;
    public RewardedAd rewarded;
    //private PlayReviewInfo _playReviewInfo;
    public List<Button> listofbuttons = new List<Button>();
    public GameObject GetCoins;
    private bool GetCoinsClicked = false;
    // Create instance of ReviewManager
    private ReviewManager _reviewManager;
    [SerializeField]
    private GameObject RateUsPanel;
    public const string AdID = "ca-app-pub-7201061393448184/4359617145"/*"ca-app-pub-3940256099942544/5224354917"*/;

    private void Start()
    {
        Time.timeScale = 1;
        //Application.targetFrameRate = 60;
        if (PlayerPrefs.GetInt("FirstTimeEntered") != 1)
        {
            SaveSystem.SaveTimeIsFirstEntered(DateTime.UtcNow);
            PlayerPrefs.SetInt("FirstTimeEntered", 1);
            // PlayerPrefs.DeleteAll();
        }
        else
        {
            if (DateTime.UtcNow.Subtract(SaveSystem.LoadTimeIsFirstEntered()).TotalDays > 1 && PlayerPrefs.GetInt("RateUsNoOrYes") != 1)
            {
                if (SaveSystem.LoadTimeIsClickedRateLater() != null)
                {
                    if (DateTime.UtcNow.Subtract((DateTime)SaveSystem.LoadTimeIsClickedRateLater()).TotalDays > 1)
                    {
                        RateUsPanel.SetActive(true);
                    }
                }
                else
                {
                    RateUsPanel.SetActive(true);
                }
            }
            // PlayerPrefs.DeleteAll();
        }
        if (SaveSystem.LoadDateAfterWatchingAd() != null)
        {
            if (DateTime.UtcNow.Subtract((DateTime)SaveSystem.LoadDateAfterWatchingAd()).TotalHours > 1)
            {
                GetCoins.SetActive(true);
                LoadAd();
            }
        }
        else
        {
            GetCoins.SetActive(true);
            LoadAd();
        }
        //PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenuPanel.activeSelf)
            {
                Application.Quit();
            }
        }

    }
    private void LoadAd()
    {
        rewarded = new RewardedAd(AdID);
        rewarded.OnAdClosed += OnAdClosed;
        rewarded.OnAdOpening += OnAdOpening;
        rewarded.OnAdFailedToLoad += OnAdFailedToLoad;
        rewarded.OnAdFailedToShow += OnAdFailedToShow;
        rewarded.OnUserEarnedReward += OnUserEarnedReward;
        rewarded.OnAdLoaded += OnAdLoaded;
        rewarded.LoadAd(new AdRequest.Builder().Build());
    }
   /* IEnumerator RequestReviews()
    {
        // Request a ReviewInfo object
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        //Launch the in-app review
        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
    }*/
    public void OnNoClicked()
    {
        RateUsPanel.SetActive(false);
        PlayerPrefs.SetInt("RateUsNoOrYes", 1);
    }
    public void OnLaterClicked()
    {
        RateUsPanel.SetActive(false);
        SaveSystem.SaveTimeIsClickedRateLater(DateTime.UtcNow);
    }
    public void OnRateUsClicked()
    {
        Application.OpenURL("market://details?id = com.INJUSTIC_INC.Cu3ber");
        RateUsPanel.SetActive(false);
        PlayerPrefs.SetInt("RateUsNoOrYes", 1);
    }
    public void OnGetCoinsClicked()
    {
        foreach (Button button in listofbuttons)
        {
            button.interactable = false;
        }
        GetCoins.transform.GetChild(1).gameObject.SetActive(false);
        GetCoins.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ad is loading...";
        StartCoroutine(WaitForAdMob());
        GetCoinsClicked = true;
        if (rewarded.IsLoaded())
        {
            rewarded.Show();
        }
    }
    private void OnAdOpening(object sender, EventArgs e)
    {
        if (GetCoinsClicked)
        {
            StopCoroutine(WaitForAdMob());
        }
    }
    private void OnAdLoaded(object sender, EventArgs e)
    {
        if (GetCoinsClicked)
        {
            rewarded.Show();
        }
    }
    private void OnUserEarnedReward(object sender, Reward e)
    {
        SaveSystem.SaveCoins(10);
        SaveSystem.SaveTimeAfterWatchingAd(DateTime.UtcNow);
        foreach (Button button in listofbuttons)
        {
            button.interactable = true;
        }
        GetCoins.SetActive(false);
        GetCoinsClicked = false;
    }

    private void OnAdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        foreach (Button button in listofbuttons)
        {
            button.interactable = true;
        }
        GetCoins.transform.GetChild(1).gameObject.SetActive(true);
        GetCoins.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "get 10";
        GetCoins.transform.localScale = new Vector3(1.25f, 2.28f, 1.24f);
        GetCoins.GetComponent<Animator>().enabled = true;
        GetCoinsClicked = false;
        LoadAd();
    }

    private void OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        foreach (Button button in listofbuttons)
        {
            button.interactable = true;
        }
        GetCoins.transform.GetChild(1).gameObject.SetActive(true);
        GetCoins.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "get 10";
        GetCoins.transform.localScale = new Vector3(1.25f, 2.28f, 1.24f);
        GetCoins.GetComponent<Animator>().enabled = true;
        GetCoinsClicked = false;
        LoadAd();
    }

    private void OnAdClosed(object sender, EventArgs e)
    {
        foreach (Button button in listofbuttons)
        {
            button.interactable = true;
        }
        GetCoins.transform.GetChild(1).gameObject.SetActive(true);
        GetCoins.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "get 10";
        GetCoins.transform.localScale = new Vector3(1.25f, 2.28f, 1.24f);
        GetCoins.GetComponent<Animator>().enabled = true;
        GetCoinsClicked = false;
        LoadAd();
    }

    public void OnAchievmentsClicked()
    {
        Cube.SetActive(false);
        MainMenuPanel.SetActive(false);
        AchievmentsPanel.SetActive(true);
    }
    public void OnBackAchievmentsClicked()
    {
        Cube.SetActive(true);
        MainMenuPanel.SetActive(true);
        AchievmentsPanel.SetActive(false);
    }
    public void OnLevelClicked(int level)
    {
        FindObjectOfType<SceneLoader>().LoadScene(level);
        ChooseLevelPanel.SetActive(false);
    }
    public void OnShopClicked()
    {
        Cube.SetActive(false);
        MainMenuPanel.SetActive(false);
        ShopPanel.SetActive(true);
    }
    public void OnBackShopClicked()
    {
        Cube.SetActive(true);
        MainMenuPanel.SetActive(true);
        ChangeShopCategory _ChangeShopCategory = FindObjectOfType<ChangeShopCategory>();
        for (int i = 0; i < _ChangeShopCategory.ShopCategories.Count; ++i)
        {
            if (_ChangeShopCategory.ShopCategories[i].activeSelf)
            {
                _ChangeShopCategory.ShopCategories[i].SetActive(false);
                _ChangeShopCategory.ShopCategories[0].SetActive(true);
                break;
            }
        }
        ShopPanel.SetActive(false);
    }
    public void OnLeaderboardClicked()
    {
        Cube.SetActive(false);
        MainMenuPanel.SetActive(false);
        LeaderboardPanel.SetActive(true);
    }
    public void OnBackLeaderboardClicked()
    {
        Cube.SetActive(true);
        MainMenuPanel.SetActive(true);
        LeaderboardPanel.SetActive(false);
    }
    public void OnPlayClicked()
    {
        Cube.SetActive(false);
        MainMenuPanel.SetActive(false);
        ChooseModePanel.SetActive(true);
    }
    public void OnSettingsClicked()
    {
        Cube.SetActive(false);
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void OnBackSettingsClicked()
    {
        Cube.SetActive(true);
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    public void OnBackChooseModeClicked()
    {
        Cube.SetActive(true);
        MainMenuPanel.SetActive(true);
        ChooseModePanel.SetActive(false);
    }
    public void OnBackChooseLevelClicked()
    {
        ChooseModePanel.SetActive(true);
        ChooseLevelPanel.SetActive(false);
    }
    public void OnUnlimitedLevelClicked()
    {
        ChooseModePanel.SetActive(false);
        FindObjectOfType<SceneLoader>().LoadScene(1);
    }
    public void OnLevelsClicked()
    {
        ChooseModePanel.SetActive(false);
        ChooseLevelPanel.SetActive(true);
    }
    public void OnCreditsClicked()
    {
        Cube.SetActive(false);
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void OnBackCreditsClicked()
    {
        Cube.SetActive(true);
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }
    public void GoToCredits(string url)
    {
        Application.OpenURL(url);
    }
    private IEnumerator WaitForAdMob()
    {
        yield return new WaitForSeconds(7);
        GetCoinsClicked = false;
        LoadAd();
        GetCoins.transform.GetChild(1).gameObject.SetActive(true);
        GetCoins.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "get 10";
        GetCoins.transform.localScale = new Vector3(1.25f, 2.28f, 1.24f);
        GetCoins.GetComponent<Animator>().enabled = true;
        foreach (Button button in listofbuttons)
        {
            button.interactable = true;
        }
    }

    private void OnApplicationQuit()
    {
        Caching.ClearCache();
    }
}
