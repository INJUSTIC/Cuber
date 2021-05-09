using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PercentVolume : MonoBehaviour
{
    public TextMeshProUGUI PercentofVolume;
    private void Update()
    {
        PercentofVolume.text = $"{(int)(Settings.volume * 100)}%".ToString();
    }
}
