using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnBGskinClicked : MonoBehaviour
{
    [HideInInspector]
    public bool IsBought = false;
    [HideInInspector]
    public bool OnClicked = false;
    private SkinManager _SkinManager;
    private BGBuyingManager _BGBuyingManager;
    private void Start()
    {
        _BGBuyingManager = FindObjectOfType<BGBuyingManager>();
        _SkinManager = FindObjectOfType<SkinManager>();
        if (this == _SkinManager.BGSkins[0])
        {
            IsBought = true;
        }
        else if (IsBought)
        {
            transform.Find("PriceContainer").gameObject.SetActive(false);
        }
        int index = SaveSystem.LoadIndexofBG();
        _SkinManager.BGSkins[index].OnClicked = true;
    }
    public void OnClickedBuy()
    {
        _BGBuyingManager.BuyingBG = gameObject;
        _BGBuyingManager.WarningPanel.SetActive(true);
    }
    private void Update()
    {
        if (OnClicked && IsBought)
        {
            transform.localScale = new Vector3(5.5f, 5.5f, 5.5f);
        }
        else
        {
            transform.localScale = new Vector3(4.1f, 4.1f, 4.1f);
        }
        if (Input.GetMouseButtonDown(0) && !_BGBuyingManager.WarningPanel.activeSelf && !_BGBuyingManager.NotEnoughCoinsPanel.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycasthit))
            {
                OnBGskinClicked onBGSkinClicked = raycasthit.collider.GetComponent<OnBGskinClicked>();
                if (onBGSkinClicked.IsBought)
                {
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
                    onBGSkinClicked.OnClickedBuy();
                }
            }
        }
    }
}
