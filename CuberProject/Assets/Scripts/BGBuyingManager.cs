using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class BGBuyingManager : MonoBehaviour
{
    public GameObject NotEnoughCoinsPanel;
    public GameObject WarningPanel;
    [HideInInspector]
    public GameObject BuyingBG;
    private SkinManager _SkinManager;
    // public VisualizerObjectScript
    private void Awake()
    {
        _SkinManager = FindObjectOfType<SkinManager>();
        List<bool> list = SaveSystem.LoadBGIsBought(_SkinManager.BGSkins.Count - 1);
        for (int i = 1; i < _SkinManager.BGSkins.Count; ++i)
        {
            _SkinManager.BGSkins[i].IsBought = list[i - 1];
        }
    }
    public void OnYesClicked()
    {
        int Price = System.Convert.ToInt32(BuyingBG.transform.Find("PriceContainer").Find("Price").GetComponent<TextMeshProUGUI>().text);
        float CurrentCoins = SaveSystem.LoadCoins();
        if (CurrentCoins >= Price)
        {
            SaveSystem.SaveCoins(-Price);
            BuyingBG.GetComponent<OnBGskinClicked>().IsBought = true;
            for (int i = 1; i < _SkinManager.BGSkins.Count; ++i)
            {
                if (_SkinManager.BGSkins[i] == BuyingBG.GetComponent<OnBGskinClicked>())
                {
                    SaveSystem.SaveBGIsBought(true, i - 1, _SkinManager.BGSkins.Count - 1);
                }
            }
            BuyingBG.transform.Find("PriceContainer").gameObject.SetActive(false);
            WarningPanel.SetActive(false);
            OnBGskinClicked onBGSkinClicked = BuyingBG.GetComponent<OnBGskinClicked>();
            for (int i = 0; i < _SkinManager.BGSkins.Count; ++i)
            {
                if (_SkinManager.BGSkins[i].OnClicked)
                {
                    _SkinManager.BGSkins[i].OnClicked = false;
                    break;
                }
            }
            SaveSystem.SaveColorofBG(onBGSkinClicked.gameObject.GetComponent<MeshRenderer>().sharedMaterial.color);
            onBGSkinClicked.OnClicked = true;
            for (int i = 0; i < _SkinManager.BGSkins.Count; ++i)
            {
                if (_SkinManager.BGSkins[i].OnClicked)
                {
                    SaveSystem.SaveIndexofBG(i);
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

