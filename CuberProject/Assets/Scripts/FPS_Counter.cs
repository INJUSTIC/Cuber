using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS_Counter : MonoBehaviour
{
    private int current;
    private void Start()
    {
        StartCoroutine(FPSCount());
    }
    private IEnumerator FPSCount()
    {
        while(true)
        {
            current = (int)(1f / Time.unscaledDeltaTime);
            GetComponent<TextMeshProUGUI>().text = current.ToString() + " FPS";
            yield return new WaitForSeconds(0.5f);
        }

    }
}
