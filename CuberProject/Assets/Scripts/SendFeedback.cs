using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFeedback : MonoBehaviour
{
    public void SendFeedbackClicked()
    {
        Application.OpenURL("mailto:outjust1c@gmail.com");
    }
}
