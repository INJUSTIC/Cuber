using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementGetting : MonoBehaviour
{
    private bool IsFirstActivated = true;
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public bool AchGot = false;
    [HideInInspector]
    public bool AchIsCompleted = false;
    public GameObject CoinImage;
    public TextMeshProUGUI AmountofCoins;
    [HideInInspector]
    public float Coins;
    AchievementsContainer _AchievementsContainer;
    private void Start()
    {
        _AchievementsContainer = FindObjectOfType<AchievementsContainer>();
        IsFirstActivated = false;
        if (AchIsCompleted && !AchGot)
        {
            transform.Find("GET").gameObject.SetActive(true);
        }
    }
    public void OnGetClicked()
    {
        transform.Find("GET").gameObject.SetActive(false);
        transform.GetComponent<Animation>().Play("AchievementsGettingAnim");
        transform.Find("CoinContainer").gameObject.SetActive(true);
        for(int i = 0; i < _AchievementsContainer.AchievmentContainer.Count; ++i)
        {
            if(this == _AchievementsContainer.AchievmentContainer[i])
            {
                SaveSystem.SaveAchievementsHasGot(true, i);
            }
        }
    }
    private void OnEnable()
    {
        if(!IsFirstActivated)
        {
            transform.localPosition = position;
        }
    }
    public void StartAnimation()
    {
        Coins = (float)System.Convert.ToDouble(AmountofCoins.text);
        CoinImage.SetActive(true);
    }
    public void AfterAnimation()
    {
        for (int i = 0; i < _AchievementsContainer.AchievmentContainer.Count;++i)
        {
            if(_AchievementsContainer.AchievmentContainer[i] == this)
            {
                for(int j = i+1; j < _AchievementsContainer.AchievmentContainer.Count; ++j)
                {
                    _AchievementsContainer.AchievmentContainer[j].transform.localPosition
                        = new Vector3(_AchievementsContainer.AchievmentContainer[j].transform.localPosition.x,
                        _AchievementsContainer.AchievmentContainer[j].transform.localPosition.y + 77f,
                        _AchievementsContainer.AchievmentContainer[j].transform.localPosition.z);
                }
            }
            _AchievementsContainer.AchievmentContainer[i].position = _AchievementsContainer.AchievmentContainer[i].transform.localPosition;
        }
    }
}
