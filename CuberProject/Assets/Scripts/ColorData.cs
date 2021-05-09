using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorData
{
    public byte[] Color;
    public ColorData(Color32 color)
    {
        Color = new byte[3];
        Color[0] = color.r;
        Color[1] = color.g;
        Color[2] = color.b;
    }
}
