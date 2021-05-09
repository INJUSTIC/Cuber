using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinAchievementMovement : MonoBehaviour
{
    private Stopwatch Wait = new Stopwatch();
    public int WaitFor;
    private const int Speed = 600;
    public GameObject[] Targets;
    private int Radius = 1;
    public TextMeshProUGUI AmountofCoins;
    public GameObject CoinImage;
    private Stopwatch WaitAfterLastCoin = new Stopwatch();
    void Start()
    {
        Wait.Start();
    }
    void Update()
    {
        CoinImage.SetActive(true);
        if (Vector2.Distance(transform.position, Targets[1].transform.position) < Radius)
        {
            if (gameObject.name == "Coin (9)" && !WaitAfterLastCoin.IsRunning)
            {
                gameObject.GetComponent<Image>().enabled = false;
                SaveSystem.SaveCoins((float)System.Convert.ToDouble(AmountofCoins.text) / 10);
                transform.parent.parent.parent.GetComponent<AchievementGetting>().Coins -= (float)System.Convert.ToDouble(AmountofCoins.text) / 10;
                WaitAfterLastCoin.Start();
            }
            else if(gameObject.name != "Coin (9)")
            {
                Destroy(gameObject);               
                SaveSystem.SaveCoins((float)System.Convert.ToDouble(AmountofCoins.text) / 10);
                transform.parent.parent.parent.GetComponent<AchievementGetting>().Coins -= (float)System.Convert.ToDouble(AmountofCoins.text) / 10;
            }
            if(WaitAfterLastCoin.ElapsedMilliseconds >= 1000)
            {
                CoinImage.SetActive(false);
                Destroy(gameObject);
            }
        }
        if (Wait.ElapsedMilliseconds >= WaitFor)
        {
            transform.position = Vector2.MoveTowards(transform.position, Targets[1].transform.position, Speed * Time.deltaTime);
        }
    }
}
