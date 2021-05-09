using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AchievementManager : MonoBehaviour
{
    //General Properties
    public GameObject AchNote;
    public GameObject AchTitle;
    public GameObject AchDesc;
    public AudioSource AchSound;
    //Ach01 Properties
    public static int Ach01CurrentScore = 0;
    public int Ach01NeedScore = 500;
    public int Ach02NeedScore = 1000;
    public int Ach03NeedScore = 2000;
    public static int Ach01CurrentCoins = 0;
    public int Ach01NeedCoins = 10;
    public int Ach02NeedCoins = 50;
    public int Ach03NeedCoins = 100;
  /*  public static int Ach01CurrentCoins = 0;
    public int Ach01NeedCoins = 10;
    public int Ach02NeedCoins = 50;
    public int Ach03NeedCoins = 100;*/

    private int Ach01Code;
    private int Ach02Code;
    private int Ach03Code;
    private int Ach04Code;
    private int Ach05Code;
    private int Ach06Code;
    private int Ach07Code;
    private int Ach08Code;
    private int Ach09Code;
    private int Ach10Code;

    private void Update()
    {
        Ach01Code = PlayerPrefs.GetInt("Ach01");
        if(Ach01CurrentScore >= Ach01NeedScore && Ach01Code != 1)
        {
            StartCoroutine(TriggerAch01());
        }
        
    }
    IEnumerator TriggerAch01()
    {
        //AchSound.Play();
        SaveSystem.SaveAchievements(true, 0);
        AchNote.SetActive(true);
        AchTitle.GetComponent<TextMeshProUGUI>().text = "Achievment Complete!";
        AchDesc.GetComponent<TextMeshProUGUI>().text = "Reach 100 points in unlimited mode";
        PlayerPrefs.SetInt("Ach01", 1);
        yield return new WaitForSeconds(5);
        AchNote.GetComponent<Animation>().Play("AchievementPanelDisappearing");
        /*AchNote.SetActive(false);
        AchTitle.GetComponent<TextMeshProUGUI>().text = "";
        AchDesc.GetComponent<TextMeshProUGUI>().text = "";*/
    }
}
