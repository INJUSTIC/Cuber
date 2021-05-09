using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopCoinsText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = SaveSystem.LoadCoins().ToString("0");
    }
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = SaveSystem.LoadCoins().ToString("0");
    }
}
