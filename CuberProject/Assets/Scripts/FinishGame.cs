using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using System.Diagnostics;

public class FinishGame : MonoBehaviour
{
    public static bool IsCompleted = false;
    public TextMeshProUGUI Coins;
    public bool IsCameraCollidesFinishObst = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /* GetComponent<AudioSource>().volume = 0.8f;
             GetComponent<AudioSource>().Play();*/
            Time.timeScale = 0.15f;
            IsCompleted = true;
            //GetComponent<MeshRenderer>().material.color = new Color32(255,254,90,255);
            string numberofscene = SceneManager.GetActiveScene().name.Remove(0, 5);
            if (!SaveSystem.LoadLevelIsCompleted(LevelManager.Count)[System.Convert.ToInt32(numberofscene) - 1])
            {
                SaveSystem.SaveCoins(System.Convert.ToInt32(Coins.text));
            }
            else
            {
                FindObjectOfType<SceneLoader>().transform.GetChild(0).Find("Your reward: ").gameObject.SetActive(false);
            }
            LevelState.Unlocking(System.Convert.ToInt32(numberofscene));
            FindObjectOfType<PauseManager>().transform.Find("Pause").gameObject.SetActive(false);
            Camera.main.GetComponent<Animation>().Play("FinishAnim");
            SaveSystem.SaveLevelIsCompleted(true, System.Convert.ToInt32(numberofscene) - 1, LevelManager.Count);
        }
        else if (other.CompareTag("MainCamera"))
        {
            //IsCameraCollidesFinishObst = true;
            ScriptCollision.IsOvered = true;
            Time.timeScale = 1f;
            Camera.main.GetComponent<Animation>().Stop("FinishAnim");
            FindObjectOfType<LoadNewGame>().GetComponent<Animation>().Play("LevelCompleteAnimation1");
        }
    }
}
