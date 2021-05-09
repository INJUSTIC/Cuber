using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsContainer : MonoBehaviour
{
    public List<AchievementGetting> AchievmentContainer = new List<AchievementGetting>();
    private bool IsFirst = true;
    public GameObject CoinImage;
    private void OnEnable()
    {
        transform.parent.localPosition = new Vector2(0, -281);
        if(!IsFirst)
        {
            for (int i = 0; i < AchievmentContainer.Count; ++i)
            {
                AchievmentContainer[i].AchIsCompleted = SaveSystem.LoadAchievements()[i];
                AchievmentContainer[i].AchGot = SaveSystem.LoadAchievementsHasGot()[i];
                if (AchievmentContainer[i].AchGot && AchievmentContainer[i].gameObject.activeSelf)
                {
                    CoinImage.SetActive(false);
                    SaveSystem.SaveCoins(AchievmentContainer[i].Coins);
                    AchievmentContainer[i].gameObject.SetActive(false);
                    for (int j = i; j < AchievmentContainer.Count; ++j)
                    {
                        AchievmentContainer[j].transform.localPosition = new Vector3(AchievmentContainer[j].transform.localPosition.x, AchievmentContainer[j].transform.localPosition.y + 77f, AchievmentContainer[j].transform.localPosition.z);
                    }
                }
                AchievmentContainer[i].position = AchievmentContainer[i].transform.localPosition;
            }
        }
        else
        {
            IsFirst = false;
        }
    }
    private void Start()
    {
        for (int i = 0; i < AchievmentContainer.Count; ++i)
        {
            AchievmentContainer[i].AchIsCompleted = SaveSystem.LoadAchievements()[i];
            AchievmentContainer[i].AchGot = SaveSystem.LoadAchievementsHasGot()[i];
            if (AchievmentContainer[i].AchGot == true)
            {
                AchievmentContainer[i].gameObject.SetActive(false);
                for (int j = i; j < AchievmentContainer.Count; ++j)
                {
                    AchievmentContainer[j].transform.localPosition = new Vector3(AchievmentContainer[j].transform.localPosition.x, AchievmentContainer[j].transform.localPosition.y + 77f, AchievmentContainer[j].transform.localPosition.z);

                }
            }
            AchievmentContainer[i].position = AchievmentContainer[i].transform.localPosition;
        }
    }
}
