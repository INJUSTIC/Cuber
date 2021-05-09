using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGColor : MonoBehaviour
{
    private void Awake()
    {
        ColorData colorData = SaveSystem.LoadColorofBG();
        //Camera.main.backgroundColor = new Color(colorData.Color[0], colorData.Color[1], colorData.Color[2]);
    }
}
