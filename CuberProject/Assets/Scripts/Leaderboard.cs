using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Transform Container;
    public Transform Leaders;
    public static List<string> ListofLeaders;
    private const float ScreenResAttitude = 2.2558f;
    private int Height = 150;
    private void OnEnable()
    {
       /* float DivideWidthAndHeight = (float)Screen.width / Screen.height;
        if (DivideWidthAndHeight > 1.65 && DivideWidthAndHeight < 1.7)
        {
            transform.GetChild(0).GetChild(0).localPosition = new Vector2(13, -526.25f);
        }
        else if (DivideWidthAndHeight > 1.75 && DivideWidthAndHeight < 1.8)
        {
            transform.GetChild(0).GetChild(0).localPosition = new Vector2(13, -538.1f);
        }
        else if (Screen.width % Screen.height == 0)
        {
            transform.GetChild(0).GetChild(0).localPosition = new Vector2(13, -558.7f);
        }
        else
        {
            transform.GetChild(0).GetChild(0).localPosition = new Vector2(13, -563.3f);
        } */
    }
    private void Start()
    {
       /* Height = (int)(200*((float)Screen.width / Screen.height / ScreenResAttitude));
        /*float DivideWidthAndHeight = (float)Screen.width / Screen.height;
        if (DivideWidthAndHeight > 1.65 && DivideWidthAndHeight < 1.7)
        {
            Height = 100;
        }
        else if (DivideWidthAndHeight > 1.75 && DivideWidthAndHeight < 1.8)
        {
            Debug.Log("WTF");
            float delete = (float)Screen.height / 720;
            Height = (int)(155 * delete);
        }
        else if (Screen.width % Screen.height == 0)
        {
            Height = 200;
        }
        else
        {
            Height = 220;
        }*/
        Leaders.gameObject.SetActive(false);
        ListofLeaders = SaveSystem.LoadScore();
        if (ListofLeaders != null) CreateLeader();
    }
    public void CreateLeader()
    {
        if (ListofLeaders.Count > 10)
        {
            ListofLeaders.RemoveRange(10, ListofLeaders.Count - 10);
        }
        for (int i = 0; i < ListofLeaders.Count; ++i)
        {
            Transform LeaderTransform = Instantiate(Leaders, Container);
            LeaderTransform.position = new Vector3(Leaders.position.x, Leaders.position.y - Height * i);
            Debug.Log(LeaderTransform.position);
            switch (i)
            {
                case 0:
                    {
                        LeaderTransform.Find("Pos").GetComponent<TextMeshProUGUI>().text = "1ST";
                        break;
                    }
                case 1:
                    {
                        LeaderTransform.Find("Pos").GetComponent<TextMeshProUGUI>().text = "2ND";
                        break;
                    }
                case 2:
                    {
                        LeaderTransform.Find("Pos").GetComponent<TextMeshProUGUI>().text = "3RD";
                        break;
                    }
                default:
                    {
                        LeaderTransform.Find("Pos").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString() + "TH";
                        break;
                    }
            }
            LeaderTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = ListofLeaders[i];
            LeaderTransform.gameObject.SetActive(true);
        }
    }
}
