using UnityEngine;
using TMPro;
using System.Diagnostics;
using System.Collections;

public class ContinuePlayAfterAd : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    //private Stopwatch Timer = new Stopwatch();
    public GameObject PartSyst;

    public void StartTimer()
    {
        PartSyst.transform.position = FindObjectOfType<MovementScript>().gameObject.transform.position + new Vector3(0, -0.9f, -0.5f);
        StartCoroutine(CountDown());
    }
    /*private void Update()
    {
        if(Timer.IsRunning)
        {
            if (FindObjectOfType<Restart>().Response != "")
            {
                if (Timer.ElapsedMilliseconds >= 500 && TimerText.text != "3")
                {
                    TimerText.text = (System.Convert.ToInt32(TimerText.text) - 1).ToString();
                    if (TimerText.text == "0")
                    {
                        ContinuePlay();
                    }
                    else Timer.Restart();
                }
                if (Timer.ElapsedMilliseconds >= 1000 && TimerText.text == "3")
                {
                    TimerText.text = "2";
                    Timer.Restart();
                }
            }
            else
            {
                if (Timer.ElapsedMilliseconds >= 500)
                {
                    TimerText.text = (System.Convert.ToInt32(TimerText.text) - 1).ToString();
                    if (TimerText.text == "0")
                    {
                        ContinuePlay();
                    }
                    else Timer.Restart();
                }
            }
        }      
    }*/
    private void ContinuePlay()
    {
        //  Timer.Stop();
        TimerText.text = "";
        ScriptCollision.IsOvered = false;
        PartSyst.SetActive(true);
        FindObjectOfType<Score>().GetComponent<TextMeshProUGUI>().enabled = true;
        FindObjectOfType<PauseManager>().transform.Find("Pause").gameObject.SetActive(true);
        FindObjectOfType<StartGame>().IsFirst = false;
        if (AudioManager.MusicState)
        {
            AudioManager.CurrentMusic = UnityEngine.Random.Range(0, FindObjectOfType<AudioManager>().sounds.Length - 1);
            FindObjectOfType<AudioManager>().Play(AudioManager.CurrentMusic);
        }
    }
    private IEnumerator CountDown()
    {
        for (int i = 3; i > 0; --i)
        {
            TimerText.text = i.ToString();
            yield return new WaitForSeconds(0.8f);
        }
        ContinuePlay();
    }
}
