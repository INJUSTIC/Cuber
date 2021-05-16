using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class CubeBuyingManager : MonoBehaviour
{
    //public List<GameObject> PriceContainers = new List<GameObject>();
    public GameObject NotEnoughCoinsPanel;
    public GameObject WarningPanel;
    [HideInInspector]
    public GameObject BuyingCube;
    private SkinManager _SkinManager;
    private void Awake()
    {
        _SkinManager = FindObjectOfType<SkinManager>();
        List<bool> list = SaveSystem.LoadCubeIsBought(_SkinManager.CubeSkins.Count - 1);
        for (int i = 1; i < _SkinManager.CubeSkins.Count; ++i)
        {
            _SkinManager.CubeSkins[i].IsBought = list[i - 1];
        }
    }
    public void OnYesClicked()
    {
        int Price = System.Convert.ToInt32(BuyingCube.transform.Find("PriceContainer").Find("Price").GetComponent<TextMeshProUGUI>().text);
        float CurrentCoins = /*SaveSystem.LoadCoins()*/200;
        if (CurrentCoins >= Price)
        {
            SaveSystem.SaveCoins(-Price);
            BuyingCube.GetComponent<OnCubeSkinClicked>().IsBought = true;
            for (int i = 1; i < _SkinManager.CubeSkins.Count; ++i)
            {
                if (_SkinManager.CubeSkins[i] == BuyingCube.GetComponent<OnCubeSkinClicked>())
                {
                    SaveSystem.SaveCubeIsBought(true, i - 1, _SkinManager.CubeSkins.Count - 1);
                }
            }
            BuyingCube.transform.Find("PriceContainer").gameObject.SetActive(false);
            WarningPanel.SetActive(false);
            OnCubeSkinClicked onCubeSkinClicked = BuyingCube.GetComponent<OnCubeSkinClicked>();
            for (int i = 0; i < _SkinManager.CubeSkins.Count; ++i)
            {
                if (_SkinManager.CubeSkins[i].OnClicked)
                {
                    _SkinManager.CubeSkins[i].OnClicked = false;
                    break;
                }
            }
            string name = onCubeSkinClicked.gameObject.name;
            if (name == "PokerFace")
            {
                SaveSystem.SaveIndexofSkinCube(0);
                PlayerPrefs.SetInt("PlayerSkin", 1);
            }
            else
            {
                PlayerPrefs.SetInt("PlayerSkin", 0);
                CubeRotation.CubeMat.color = onCubeSkinClicked.gameObject.GetComponent<MeshRenderer>().sharedMaterial.color;
                SaveSystem.SaveEmissionColorofCube(onCubeSkinClicked.gameObject.GetComponent<MeshRenderer>().sharedMaterial.GetColor("_EmissionColor"));
                SaveSystem.SaveColorofCube(onCubeSkinClicked.gameObject.GetComponent<MeshRenderer>().sharedMaterial.color);
            }
            onCubeSkinClicked.OnClicked = true;
            for (int i = 0; i < _SkinManager.CubeSkins.Count; ++i)
            {
                if (_SkinManager.CubeSkins[i].OnClicked)
                {
                    SaveSystem.SaveIndexofCube(i);
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
