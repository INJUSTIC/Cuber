using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject StartPanel;
    // public TextMeshProUGUI AchievePoints;
    public bool IsFirst = true;
    public static bool IsFirstCalled = true;
    public GameObject FinishObstacle;
    public static int AttemptNumber;
    private void Start()
    {
        // Application.targetFrameRate = 60;
        PlayerColor.IsPartSystActive = false;
        /* if (SceneManager.GetActiveScene().name != "UnlimitedLevel")
         {
             AchievePoints.text = (FinishObstacle.transform.position.z / 5).ToString("0");
         }*/
        if (IsFirstCalled)
        {
            IsFirstCalled = false;
            if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
            {
                FindObjectOfType<Restart>().CoinImage.SetActive(false);
                FindObjectOfType<Score>().GetComponent<TextMeshProUGUI>().enabled = false;
            }
            else
            {
                AttemptNumber = 1;
            }
            FindObjectOfType<PauseManager>().transform.Find("Pause").gameObject.SetActive(false);
            StartPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            AttemptNumber++;
            StartPlay();
        }
    }
    public void StartPlay()
    {
        PlayerColor.IsPartSystActive = true;
        IsFirst = false;
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            FindObjectOfType<Score>().GetComponent<TextMeshProUGUI>().enabled = true;
            StartCoroutine(FindObjectOfType<MovementScript>().IncreaseSpeedUnLev());
        }
        FindObjectOfType<AudioManager>().Play(5/*AudioManager.CurrentMusic*/);
        FindObjectOfType<PauseManager>().transform.Find("Pause").gameObject.SetActive(true);
        StartPanel.SetActive(false);
    }
}
