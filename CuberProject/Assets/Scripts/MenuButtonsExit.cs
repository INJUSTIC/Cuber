using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonsExit : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public void OnPointerExit(PointerEventData eventData)
    {
        Button button = gameObject.GetComponent<Button>();
        ColorBlock cb = button.colors;
        cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, 0);
        button.colors = cb;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Button button = gameObject.GetComponent<Button>();
        ColorBlock cb = button.colors;
        cb.pressedColor = new Color(cb.pressedColor.r, cb.pressedColor.g, cb.pressedColor.b, 0.24f);
        button.colors = cb;
    }
}
