using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public List<OnCubeSkinClicked> CubeSkins = new List<OnCubeSkinClicked>();
    public List<OnBGskinClicked> BGSkins = new List<OnBGskinClicked>();
    public List<OnLandSkinClicked> LandSkins = new List<OnLandSkinClicked>();
}
