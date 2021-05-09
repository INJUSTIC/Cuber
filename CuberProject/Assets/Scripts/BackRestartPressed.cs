using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackRestartPressed : MonoBehaviour, IPointerUpHandler
{
    [HideInInspector]
    public bool IsBackPressed = false;
    public GameObject SaveMeForAdButton;
    public void OnPointerUp(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name != "UnlimitedLevel" || (SceneManager.GetActiveScene().name == "UnlimitedLevel" && !SaveMeForAdButton.GetComponent<AnimatingButtons>().IsButtonUp))
            IsBackPressed = true;
    }
}
