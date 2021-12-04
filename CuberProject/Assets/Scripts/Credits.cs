using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void SendFeedbackClicked()
    {
        Application.OpenURL("mailto:outjust1c@gmail.com");
    }
    public void OnURLCliked(string URL)
    {
        Application.OpenURL(URL);
    }
}
