using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlaySignIn : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }
        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                UnityEngine.Debug.Log("Log in successfully");
            }
            else
            {
                UnityEngine.Debug.Log("Log in fail");
            }
        });
    }
}
