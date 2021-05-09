using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLandSkinClicked : MonoBehaviour
{
    [HideInInspector]
    public bool IsBought = false;
    [HideInInspector]
    public bool OnClicked = false;
    private SkinManager _SkinManager;
    private LandBuyingManager _LandBuyingManager;
    private void Start()
    {
        _SkinManager = FindObjectOfType<SkinManager>();
        _LandBuyingManager = FindObjectOfType<LandBuyingManager>();
        if (this == _SkinManager.LandSkins[0])
        {
            IsBought = true;
        }
        else if (IsBought)
        {
            transform.Find("PriceContainer").gameObject.SetActive(false);
        }
        int index = SaveSystem.LoadIndexofLand();
        _SkinManager.LandSkins[index].OnClicked = true;
    }
    public void OnClickedBuy()
    {
        _LandBuyingManager.BuyingLand = gameObject;
        _LandBuyingManager.WarningPanel.SetActive(true);
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
        if (Input.GetMouseButtonDown(0) && !_LandBuyingManager.WarningPanel.activeSelf && !_LandBuyingManager.NotEnoughCoinsPanel.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycasthit))
            {
                OnLandSkinClicked onLandSkinClicked = raycasthit.collider.GetComponent<OnLandSkinClicked>();
                if (this == onLandSkinClicked)
                {
                    if (onLandSkinClicked.IsBought)
                    {
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
                        onLandSkinClicked.OnClickedBuy();
                    }
                }
            }
        }
    }
}
