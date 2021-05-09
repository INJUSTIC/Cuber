using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCubeSkinClicked : MonoBehaviour
{
    [HideInInspector]
    public bool IsBought = false;
    [HideInInspector]
    public bool OnClicked = false;
    private Quaternion DefaultRotation;
    private SkinManager _SkinManager;
    private CubeBuyingManager _CubeBuyingManager;
    public void OnClickedBuy()
    {
        _CubeBuyingManager.BuyingCube = gameObject;
        _CubeBuyingManager.WarningPanel.SetActive(true);
    }
    private void Start()
    {
        _SkinManager = FindObjectOfType<SkinManager>();
        _CubeBuyingManager = FindObjectOfType<CubeBuyingManager>();
        DefaultRotation = transform.rotation;
        if (this == _SkinManager.CubeSkins[0])
        {
            IsBought = true;
        }
        else if (IsBought)
        {
            transform.Find("PriceContainer").gameObject.SetActive(false);
        }
        int index = SaveSystem.LoadIndexofCube();
        if (_SkinManager.CubeSkins[index].IsBought)
        {
            _SkinManager.CubeSkins[index].OnClicked = true;
        }
        else
        {
            CubeRotation.CubeMat.color = _SkinManager.CubeSkins[0].gameObject.GetComponent<MeshRenderer>().sharedMaterial.color;
            SaveSystem.SaveColorofCube(_SkinManager.CubeSkins[0].gameObject.GetComponent<MeshRenderer>().sharedMaterial.color);
            _SkinManager.CubeSkins[0].OnClicked = true;
        }
    }
    private void OnDisable()
    {
        transform.rotation = DefaultRotation;
    }
    private void Update()
    {
        if (OnClicked && IsBought)
        {
            transform.Rotate(0, 0.3f, 0);
        }
        if (Input.GetMouseButtonDown(0) && !_CubeBuyingManager.WarningPanel.activeSelf && !_CubeBuyingManager.NotEnoughCoinsPanel.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycasthit))
            {
                OnCubeSkinClicked onCubeSkinClicked = raycasthit.collider.GetComponent<OnCubeSkinClicked>();
                if (this == onCubeSkinClicked)
                {
                    if (onCubeSkinClicked.IsBought)
                    {
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
                        onCubeSkinClicked.OnClickedBuy();
                    }
                }
            }
        }
    }
}
