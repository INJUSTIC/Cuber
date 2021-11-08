using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Transform Player;
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        text.text = ((Player.position.z) / 5).ToString("0");
        // For Achievement 1
        /* if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
         {
             AchievementManager.Ach01CurrentScore = (int)Player.position.z / 5;
         }*/
    }
}
