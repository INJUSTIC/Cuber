using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelState> Levels = new List<LevelState>();
    public static int Count;
    private void Start()
    {
        Count = Levels.Count;
    }
}
