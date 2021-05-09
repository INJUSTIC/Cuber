using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelState : MonoBehaviour
{
    [HideInInspector]
    public bool IsOpened = false;
    public bool IsCompleted = false;
    private LevelManager _LevelManager;
    private void Start()
    {
        _LevelManager = FindObjectOfType<LevelManager>();
        for (int i = 0; i < _LevelManager.Levels.Count; ++i)
        {
            if (this == _LevelManager.Levels[i])
            {
                IsCompleted = SaveSystem.LoadLevelIsCompleted(_LevelManager.Levels.Count)[i];
                IsOpened = SaveSystem.LoadLevelIsOpened(_LevelManager.Levels.Count)[i];
                if (!IsOpened)
                {
                    GetComponent<Button>().interactable = false;
                    transform.Find("Number").GetComponent<TextMeshProUGUI>().color = new Color(transform.Find("Number").GetComponent<TextMeshProUGUI>().color.r, transform.Find("Number").GetComponent<TextMeshProUGUI>().color.g, transform.Find("Number").GetComponent<TextMeshProUGUI>().color.b, 0.5f);
                }
            }
        }
    }
    public static void Unlocking(int index)
    {
        if(index < LevelManager.Count)
        SaveSystem.SaveLevelIsOpened(true, index, LevelManager.Count);
    }
    public static bool LevelIsOpened(int level)
    {
        return SaveSystem.LoadLevelIsOpened(LevelManager.Count)[level-1];
    }
    public static bool LevelIsCompleted(int level)
    {
        return SaveSystem.LoadLevelIsCompleted(LevelManager.Count)[level - 1];
    }
}
