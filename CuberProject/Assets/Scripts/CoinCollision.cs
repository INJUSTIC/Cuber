using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinCollision : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public GameObject CoinImage;
    public TextMeshProUGUI CoinNumber;
    public static int Counter = 0;
    AudioSource Audio;
    private void Start()
    {
        Counter = 0;
        GetComponent<AudioSource>().volume = 1f;
        Audio = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoinTag"))
        {
            Destroy(other.gameObject);
            if (AudioManager.EffectsState)
            {
                Audio.Play();
            }
            Counter++;
            CoinText.text = Counter.ToString();
            StopAllCoroutines();
            StartCoroutine(CoinImageEnable());
        }
    }
    private IEnumerator CoinImageEnable()
    {
        Image _CoinImage = CoinImage.GetComponent<Image>();
        _CoinImage.color = new Color(_CoinImage.color.r, _CoinImage.color.g, _CoinImage.color.b, 1);
        CoinNumber.alpha = 1;
        CoinImage.SetActive(true);
        yield return new WaitForSeconds(2);
        //CoinImage.GetComponent<Animation>().Play("CoinImageTransparation");
        while (CoinNumber.alpha > 0)
        {
            CoinNumber.alpha -= 0.05f;
            _CoinImage.color = new Color(_CoinImage.color.r, _CoinImage.color.g, _CoinImage.color.b, _CoinImage.color.a - 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        CoinImage.SetActive(false);
    }
    /* [SerializeField]
     public void AfterCoinsImageAnim()
     {

     }*/
}
