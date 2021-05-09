using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandBuyingManager : MonoBehaviour
{
    public GameObject NotEnoughCoinsPanel;
    public GameObject WarningPanel;
    [HideInInspector]
    public GameObject BuyingLand;
    private SkinManager _SkinManager;
    private void Awake()
    {
        _SkinManager = FindObjectOfType<SkinManager>();
        List<bool> list = SaveSystem.LoadLandIsBought(_SkinManager.LandSkins.Count - 1);
        for (int i = 1; i < _SkinManager.LandSkins.Count; ++i)
        {
            _SkinManager.LandSkins[i].IsBought = list[i - 1];
        }
    }
    public void OnYesClicked()
    {
        int Price = System.Convert.ToInt32(BuyingLand.transform.Find("PriceContainer").Find("Price").GetComponent<TextMeshProUGUI>().text);
        float CurrentCoins = /*SaveSystem.LoadCoins()*/200;
        if (CurrentCoins >= Price)
        {
            SaveSystem.SaveCoins(-Price);
            BuyingLand.GetComponent<OnLandSkinClicked>().IsBought = true;
            for (int i = 1; i < _SkinManager.LandSkins.Count; ++i)
            {
                if (_SkinManager.LandSkins[i] == BuyingLand.GetComponent<OnLandSkinClicked>())
                {
                    SaveSystem.SaveLandIsBought(true, i - 1, _SkinManager.LandSkins.Count - 1);
                }
            }
            BuyingLand.transform.Find("PriceContainer").gameObject.SetActive(false);
            WarningPanel.SetActive(false);
            OnLandSkinClicked onLandSkinClicked = BuyingLand.GetComponent<OnLandSkinClicked>();
            for (int i = 0; i < _SkinManager.LandSkins.Count; ++i)
            {
                if (_SkinManager.LandSkins[i].OnClicked)
                {
                    _SkinManager.LandSkins[i].OnClicked = false;
                    break;
                }
            }
            SaveSystem.SaveColorofLand(onLandSkinClicked.gameObject.GetComponent<MeshRenderer>().sharedMaterial.color);
            onLandSkinClicked.OnClicked = true;
            for (int i = 0; i < _SkinManager.LandSkins.Count; ++i)
            {
                if (_SkinManager.LandSkins[i].OnClicked)
                {
                    SaveSystem.SaveIndexofLand(i);
                }
            }
        }
        else
        {
            WarningPanel.SetActive(false);
            NotEnoughCoinsPanel.SetActive(true);
        }
    }
    public void OnNoClicked()
    {
        WarningPanel.SetActive(false);
    }
    public void OnOKClicked()
    {
        NotEnoughCoinsPanel.SetActive(false);
    }
}
