using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.Net;
using System.Threading;
using GoogleMobileAds.Api;

public class Restart : MonoBehaviour
{
    public Material TunnelMat;
    public List<TextMeshProUGUI> ListofColorChangeableWords = new List<TextMeshProUGUI>();
    public GameObject RestartPanel;
    public GameObject SaveMeForAdButton;
    public GameObject SaveMeForCoinsButton;
    public Sprite BlackBack;
    public Sprite WhiteBack;
    public TextMeshProUGUI FinalScore;
    private static List<string> FinalScores;
    public GameObject CoinImage;
    //  public TextMeshProUGUI NeededScore;
    private static int TimesToAdd = 0;
    private RewardedAd rewardedAd;
    public GameObject Player;
    [HideInInspector]
    public bool IsFirstTimeContinuePlaying = true;
    public TextMeshProUGUI Record;
    //AdRequest request;
    public GameObject ContainerPartsofPlayer;
    public Button BackButton;
    private bool SaveMeClicked = false;
    [HideInInspector]
    public string Response;
    //   private Stopwatch TimeAfterSaveMeClicked = new Stopwatch();
    private bool IsInternetConnected = false;
    private bool IsTouchingWasBegan = false;
    private Thread ThreadForInternetConnection;
    public TextMeshProUGUI Attempt;
    public GameObject FirstPart;
    private Animation DeathAnim;
    private AnimatingButtons forAdAnimatingButton;
    private AnimatingButtons forCoinsAnimatingButton;
    private BackRestartPressed backRestartPressed;
    // private static bool IsFirstTimeAddShow = true;
    //private const string GameID = "ca-app-pub-7201061393448184~3407023356";
    private const string rewardedAdID = "ca-app-pub-7201061393448184/4508352034" /*"ca-app-pub-3940256099942544/5224354917"*/;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            forAdAnimatingButton = SaveMeForAdButton.GetComponent<AnimatingButtons>();
            forCoinsAnimatingButton = SaveMeForCoinsButton.GetComponent<AnimatingButtons>();
        }
        
        backRestartPressed = BackButton.gameObject.GetComponent<BackRestartPressed>();
        DeathAnim = GetComponent<Animation>();
        ThreadForInternetConnection = new Thread(new ThreadStart(() =>
        {
            WebClient Client = new WebClient();
            try
            {
                Response = Client.DownloadString("http://www.google.com");
                IsInternetConnected = true;
            }
            catch
            {
                Debug.Log("No Internet connection");
            }
        }));
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel" && IsFirstTimeContinuePlaying)
        {
            try
            {
                //StartCoroutine(InternetConnection());
                ThreadForInternetConnection.Start();
            }
            catch
            {

            }
        }
        if (SceneManager.GetActiveScene().name != "UnlimitedLevel" && Advertisement.isSupported && TimesToAdd >= 4)
        {
            Advertisement.Initialize("3738059", false);
        }
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel" && IsFirstTimeContinuePlaying)
        {
            LoadAd();
        }
        /* if(Camera.main.backgroundColor == new Color(0, 0, 0))
         {
             BackButton.GetComponent<Image>().sprite = WhiteBack;
         }*/
    }

    /*async private void InternetConnection()
    {
        WebClient Client = new WebClient();
        await Task.Run(() => {
            Response = Client.DownloadString("http://www.google.com");
            IsInternetConnected = true;
        });     
        //yield return null;
    }*/
    public void RestartGame()
    {
        if (ThreadForInternetConnection.IsAlive)
        {
            ThreadForInternetConnection.Abort();
        }
        if (SceneManager.GetActiveScene().name != "UnlimitedLevel")
        {
            Attempt.text = StartGame.AttemptNumber.ToString();
            TimesToAdd++;
            BackButton.interactable = false;
            if (TimesToAdd == 5)
            {
                if (Advertisement.IsReady())
                {
                    // IsFirstTimeAddShow = false;
                    Advertisement.Show();
                    BackButton.interactable = true;
                    TimesToAdd = 0;
                }
                else
                {
                    BackButton.interactable = true;
                }
            }
            else if (TimesToAdd > 5)
            {
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                    TimesToAdd = 0;
                    // IsFirstTimeAddShow = false;
                    BackButton.interactable = true;
                }
                else
                {
                    BackButton.interactable = true;
                }
            }
            else
            {
                BackButton.interactable = true;
            }
        }
        if (AudioManager.EffectsState)
        {
            GetComponent<AudioSource>().volume = 0.6f;
            GetComponent<AudioSource>().Play();
        }
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            if (IsInternetConnected)
            {
                if (!IsFirstTimeContinuePlaying)
                {
                    SaveMeForAdButton.SetActive(false);
                    SaveMeForCoinsButton.SetActive(false);
                }
                else
                {
                    SaveMeForAdButton.SetActive(true);
                    //  LoadAd();
                }
            }
            else
            {
                if (SaveSystem.LoadCoins() >= 15 && IsFirstTimeContinuePlaying)
                {
                    SaveMeForCoinsButton.SetActive(true);
                }
                else
                {
                    SaveMeForAdButton.SetActive(false);
                    SaveMeForCoinsButton.SetActive(false);
                }
            }
            string RecordText;
            Score _Score = FindObjectOfType<Score>();
            if (SaveSystem.LoadScore() != null)
            {
                RecordText = SaveSystem.LoadScore()[0];
                if (Convert.ToInt32(RecordText) > Convert.ToInt32(_Score.GetComponent<TextMeshProUGUI>().text))
                    Record.text = RecordText;
                else Record.text = _Score.GetComponent<TextMeshProUGUI>().text;
            }
            else
            {
                Record.text = _Score.GetComponent<TextMeshProUGUI>().text;
            }
            CoinImage.SetActive(false);
            FinalScore.text = _Score.GetComponent<TextMeshProUGUI>().text;
            _Score.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        /*else
        {
            NeededScore.text = FindObjectOfType<StartGame>().AchievePoints.text;
        }*/
        FindObjectOfType<PauseManager>().transform.Find("Pause").gameObject.SetActive(false);
        if (TunnelMat.color == Color.black)
        {
            BackButton.GetComponent<Image>().sprite = WhiteBack;
            for (int i = 0; i < ListofColorChangeableWords.Count; ++i)
            {
                ListofColorChangeableWords[i].color = new Color(0.9f, 0.9f, 0.9f);
                ListofColorChangeableWords[i].fontMaterial.SetColor("_UnderlayColor", Color.black);
            }
        }
        else
        {
            BackButton.GetComponent<Image>().sprite = BlackBack;
            for (int i = 0; i < ListofColorChangeableWords.Count; ++i)
            {
                ListofColorChangeableWords[i].color = new Color(0, 0, 0);
                ListofColorChangeableWords[i].fontMaterial.SetColor("_UnderlayColor", Color.white);
            }
        }
        RestartPanel.SetActive(true);
        GetComponent<Animation>().Play("DeathAnim");
    }

    public void OnSaveMeForCoinsClicked()
    {
        SaveSystem.SaveCoins(-15);
        RestartPanel.SetActive(false);
        SaveMeForCoinsButton.SetActive(false);
        ContinuePlay();
    }

    public void OnSaveMeForAdClicked()
    {
        StartCoroutine(WaitForAdMob());
        SaveMeClicked = true;

        //AdMob reward ad after death in Unlimited Mode
        //  rewardedAd = new RewardBasedVideoAd(rewardedAdID);

        //request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(SystemInfo.deviceUniqueIdentifier.ToUpper()).Build();

        //StartCoroutine(LoadAddAsynchronously());
        SaveMeForAdButton.GetComponent<Button>().interactable = false;
        BackButton.interactable = false;
        SaveMeForAdButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ad is loading...";
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
    private void LoadAd()
    {
        rewardedAd = new RewardedAd(rewardedAdID);
        rewardedAd.OnAdClosed += OnAdClosed;
        rewardedAd.OnUserEarnedReward += OnUserEarnedReward;
        rewardedAd.OnAdFailedToLoad += OnAdFailedToLoad;
        rewardedAd.OnAdFailedToShow += OnAdFailedToShow;
        rewardedAd.OnAdLoaded += OnAdLoaded;
        rewardedAd.OnAdOpening += OnAdOpening;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }
    private void OnAdOpening(object sender, EventArgs e)
    {
        if (SaveMeClicked)
        {
            StopCoroutine(WaitForAdMob());
            RestartPanel.SetActive(false);
        }
    }

    private void OnAdLoaded(object sender, EventArgs e)
    {
        if (SaveMeClicked)
        {
            rewardedAd.Show();
        }
    }
    private void OnAdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        BackButton.interactable = true;
        SaveMeForAdButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "save me!";
        SaveMeForAdButton.GetComponent<Button>().interactable = true;
        SaveMeForAdButton.GetComponent<Animator>().enabled = true;
        SaveMeForAdButton.transform.localScale = new Vector3(2.55f, 1.89f, 1.456f);
        SaveMeForAdButton.GetComponent<AnimatingButtons>().IsButtonUp = false;
        SaveMeClicked = false;
        if (IsInternetConnected)
        {
            LoadAd();
        }
    }
    private void OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        BackButton.interactable = true;
        SaveMeForAdButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "save me!";
        SaveMeForAdButton.GetComponent<Button>().interactable = true;
        SaveMeForAdButton.GetComponent<Animator>().enabled = true;
        SaveMeForAdButton.transform.localScale = new Vector3(2.55f, 1.89f, 1.456f);
        SaveMeForAdButton.GetComponent<AnimatingButtons>().IsButtonUp = false;
        SaveMeClicked = false;
        LoadAd();
    }
    private void OnAdClosed(object sender, EventArgs e)
    {
        BackButton.interactable = true;
        SaveMeForAdButton.GetComponent<Button>().interactable = true;
        if (IsFirstTimeContinuePlaying)
        {
            RestartPanel.SetActive(true);
            SaveMeForAdButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "save me!";
            SaveMeClicked = false;
            SaveMeForAdButton.GetComponent<Animator>().enabled = true;
            SaveMeForAdButton.transform.localScale = new Vector3(2.55f, 1.89f, 1.456f);
            LoadAd();
        }
        else
        {
            RestartPanel.SetActive(false);
        }
    }
    private void OnUserEarnedReward(object sender, EventArgs args)
    {
        SaveMeForAdButton.SetActive(false);
        ContinuePlay();
    }

    private void ContinuePlay()
    {
        IsTouchingWasBegan = false;
        IsFirstTimeContinuePlaying = false;
        Player.SetActive(true);
        Player.transform.position = new Vector3(0, 0, Player.transform.position.z);
        LevelGenerate _LevelGenerate = FindObjectOfType<LevelGenerate>();
        for (int i = _LevelGenerate.ActiveParts.Count - 1; i >= 0; --i)
        {
            _LevelGenerate.DeletePart();
        }
        GameObject _FirstPart = Instantiate(FirstPart);
        _LevelGenerate.FirstPart = FirstPart;
        _FirstPart.transform.position = new Vector3(-0.48f, 3.45f, Player.transform.position.z + 42.7f);
        _FirstPart.transform.SetParent(_LevelGenerate.transform);
        _LevelGenerate.ActiveParts.Add(_FirstPart);
        _LevelGenerate.SpawnZ = Player.transform.position.z + 36;
        _LevelGenerate.ListOfLastParts.Clear();
        for (int i = 0; i < 2; ++i)
        {
            _LevelGenerate.SpawnPart(_LevelGenerate.Randoming());
        }
        PlayerColor.IsPartSystActive = true;
        ContainerPartsofPlayer.SetActive(false);
        ContainerPartsofPlayer.transform.GetChild(0).localPosition = new Vector3(-0.24f, -0.24f, -0.24f);
        ContainerPartsofPlayer.transform.GetChild(0).localRotation = new Quaternion(0, 0, 0, 0);
        ContainerPartsofPlayer.transform.GetChild(1).localPosition = new Vector3(-0.24f, 0.24f, -0.3f);
        ContainerPartsofPlayer.transform.GetChild(1).localRotation = new Quaternion(0, 0, 0, 0);
        ContainerPartsofPlayer.transform.GetChild(2).localPosition = new Vector3(-0.24f, -0.24f, 0.24f);
        ContainerPartsofPlayer.transform.GetChild(2).localRotation = new Quaternion(0, 0, 0, 0);
        ContainerPartsofPlayer.transform.GetChild(3).localPosition = new Vector3(-0.24f, 0.24f, 0.3f);
        ContainerPartsofPlayer.transform.GetChild(3).localRotation = new Quaternion(0, 0, 0, 0);
        ContainerPartsofPlayer.transform.GetChild(4).localPosition = new Vector3(0.24f, -0.24f, -0.24f);
        ContainerPartsofPlayer.transform.GetChild(4).localRotation = new Quaternion(0, 0, 0, 0);
        ContainerPartsofPlayer.transform.GetChild(5).localPosition = new Vector3(0.24f, -0.24f, 0.24f);
        ContainerPartsofPlayer.transform.GetChild(5).localRotation = new Quaternion(0, 0, 0, 0);
        ContainerPartsofPlayer.transform.GetChild(6).localPosition = new Vector3(0.24f, 0.24f, -0.3f);
        ContainerPartsofPlayer.transform.GetChild(6).localRotation = new Quaternion(0, 0, 0, 0);
        ContainerPartsofPlayer.transform.GetChild(7).localPosition = new Vector3(0.24f, 0.24f, 0.3f);
        ContainerPartsofPlayer.transform.GetChild(7).localRotation = new Quaternion(0, 0, 0, 0);
        FindObjectOfType<ContinuePlayAfterAd>().StartTimer();
    }

    public void ClickRestart()
    {
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            FinalScores = SaveSystem.LoadScore() as List<string>;
            if (FinalScores == null)
            {
                FinalScores = new List<string>();
            }
            FinalScores.Add(FinalScore.text);
            SaveSystem.SaveScore(FinalScores);
            SaveSystem.SaveCoins(CoinCollision.Counter);
        }
        ScriptCollision.IsOvered = false;
        LoadScene.Load("same");
        Time.timeScale = 1f;
    }
    public void ClickMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            FinalScores = SaveSystem.LoadScore() as List<string>;
            if (FinalScores == null)
            {
                FinalScores = new List<string>();
            }
            FinalScores.Add(FinalScore.text);
            SaveSystem.SaveScore(FinalScores);
            SaveSystem.SaveCoins(CoinCollision.Counter);
        }
        ScriptCollision.IsOvered = false;
        Camera.main.GetComponent<FastMobileBloom>().enabled = false;
        Camera.main.GetComponent<FollowPlayer>().enabled = false;
        FindObjectOfType<SceneLoader>().LoadScene(0);
        gameObject.SetActive(false);
    }
    private void Update()
    {

        /*if (SaveMeClicked && TimeAfterSaveMeClicked.ElapsedMilliseconds >= 5000 && !rewardedAd.IsLoaded())
        {
            BackButton.interactable = true;
            SaveMeForAdButton.GetComponent<Button>().interactable = true;
            SaveMeForAdButton.GetComponent<SaveMeButtonPressed>().IsSaveMeButtonUp = false;
            TimeAfterSaveMeClicked.Stop();
        }*/
        if (RestartPanel.activeSelf && Input.touchCount > 0 && !Advertisement.isShowing && !DeathAnim.IsPlaying("DeathAnim"))
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                IsTouchingWasBegan = true;
            }
            if (touch.phase == TouchPhase.Ended && IsTouchingWasBegan && !backRestartPressed.IsBackPressed)
            {
                if (SceneManager.GetActiveScene().name != "UnlimitedLevel")
                {
                    ClickRestart();
                }
                else if ((!forAdAnimatingButton.IsButtonUp && !forCoinsAnimatingButton.IsButtonUp) || !IsFirstTimeContinuePlaying)
                {
                    ClickRestart();
                }
            }
        }
    }
    private IEnumerator WaitForAdMob()
    {
        yield return new WaitForSeconds(7);
        SaveMeClicked = false;
        LoadAd();
        SaveMeForAdButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "save me!";
        BackButton.interactable = true;
        SaveMeForAdButton.GetComponent<Animator>().enabled = true;
        SaveMeForAdButton.transform.localScale = new Vector3(2.55f, 1.89f, 1.456f);
        SaveMeForAdButton.GetComponent<Button>().interactable = true;
        SaveMeForAdButton.GetComponent<AnimatingButtons>().IsButtonUp = false;
        SaveMeForAdButton.GetComponent<AnimatingButtons>().IsButtonExit = false;
    }
    private void OnApplicationQuit()
    {
        Caching.ClearCache();
    }
}
