using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBGMoving : MonoBehaviour
{
    private Vector2 Speed;
    void Start()
    {

    }
    void Update()
    {      
        Speed = new Vector2(0.01f * Time.time, 0);
        GetComponent<Renderer>().material.mainTextureOffset = Speed;
    }
}
