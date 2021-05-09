using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShopCategory : MonoBehaviour
{
    public List<GameObject> ShopCategories = new List<GameObject>();

    public void OnNextArrowClicked()
    {
        for (int i = 0; i < ShopCategories.Count; ++i)
        {
            if (ShopCategories[i].activeSelf)
            {
                ShopCategories[i].SetActive(false);
                ShopCategories[i + 1].SetActive(true);
                break;
            }
        }
    }
    public void OnBackArrowClicked()
    {
        for (int i = 0; i < ShopCategories.Count; ++i)
        {
            if (ShopCategories[i].activeSelf)
            {
                ShopCategories[i].SetActive(false);
                ShopCategories[i - 1].SetActive(true);
            }
        }
    }
}
