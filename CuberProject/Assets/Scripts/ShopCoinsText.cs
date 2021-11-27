using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopCoinsText : MonoBehaviour
{
    private TextMeshProUGUI Text;
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.text = SaveSystem.LoadCoins().ToString("0");
    }
    void Update()
    {
        Text.text = SaveSystem.LoadCoins().ToString("0");
    }
}
